using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Background_Controller : MonoBehaviour
{
    const float pi = Mathf.PI;

    [SerializeField] Light2D backgroundLight;
    [SerializeField] Light2D sunLight, moonLight;
    bool dayLight, nightLight;

    [SerializeField] float sunSpeed;
    [SerializeField] float moonSpeed;
    [SerializeField] float cloudsSpeed;
    [SerializeField] float backgroundSpeed;
    [SerializeField] float foregroundSpeed;

    GameObject Sun;
    GameObject Moon;
    [SerializeField] float sunAngle;
    float moonAngle;
    float celestialRadius;
    Vector2 celestialAnchor;

    GameObject Sky;
    GameObject[] Clouds;
    GameObject[] Background;
    GameObject[] Foreground;
    
    void Awake()
    {
        Sky = transform.Find("Sky").gameObject;
        Clouds = new GameObject[2];
        Background = new GameObject[2];
        Foreground = new GameObject[2];

        Clouds[0] = transform.Find("Clouds_1").gameObject;
        Clouds[1] = transform.Find("Clouds_2").gameObject;
        Background[0] = transform.Find("Background_1").gameObject;
        Background[1] = transform.Find("Background_2").gameObject;
        Foreground[0] = transform.Find("Foreground_1").gameObject;
        Foreground[1] = transform.Find("Foreground_2").gameObject;
        
        Sun = transform.Find("Sun").gameObject;
        Moon = transform.Find("Moon").gameObject;
        sunAngle = 0;
        moonAngle = pi;
        celestialRadius = 6.5f;
        celestialAnchor = new Vector2(0, -3);
    }

    void Update()
    {
        MoveCelestial(ref Sun, ref sunAngle, sunSpeed);
        MoveCelestial(ref Moon, ref moonAngle, moonSpeed);
        MovePartOfBackground(ref Clouds, cloudsSpeed);
        MovePartOfBackground(ref Background, backgroundSpeed);
        MovePartOfBackground(ref Foreground, foregroundSpeed);

        UpdateLighting(sunAngle);

        sunLight.intensity = sunAngle > pi/2 && sunAngle < pi*3/2 ? 0 : .3f;
        moonLight.intensity = moonAngle > pi/2 && moonAngle < pi*3/2 ? 0 : .3f;
    }

    void MovePartOfBackground(ref GameObject[] parts, float speed) {
        Vector3 frameMove = Vector3.left * speed * Time.deltaTime;

        parts[0].transform.localPosition += frameMove;
        parts[1].transform.localPosition += frameMove;

        if (parts[0].transform.localPosition.x <= -16) {
            parts[0].transform.localPosition += Vector3.right * 32;

            GameObject temp = parts[0];
            parts[0] = parts[1];
            parts[1] = temp;
        }
    }

    void MoveCelestial(ref GameObject celestial, ref float angle, float speed) {
        angle += speed * Time.deltaTime;
        if (angle >= 2 * pi) {
            angle -= 2 * pi;
        }

        float xPos = celestialAnchor.x + celestialRadius * Mathf.Sin(angle);
        float yPos = celestialAnchor.y + celestialRadius * Mathf.Cos(angle);

        celestial.transform.position = new Vector3(xPos, yPos, 0);
    }

    void UpdateLighting(float angle) {
        float colorsGB = -1;
        float intensity = -1;

        if (angle > pi*19/12 || angle < pi*5/12) { // full day
            if (!dayLight) {
                backgroundLight.color = Color.white;
                backgroundLight.intensity = .8f;

                dayLight = true;
                nightLight = false;
            }

        } else if (angle < pi*7/12) { // transition from dayLight to nightLight
            if (angle < pi/2) {
                colorsGB = 1-((angle-pi*5/12)/(pi/12)*.6f);
                backgroundLight.color = new Color(1, colorsGB, colorsGB);
            } else {
                colorsGB = (angle-pi/2)/(pi/12)*.6f+.4f;
                backgroundLight.color = new Color(1, colorsGB, colorsGB);
            }
            
            intensity = 1-((angle-pi*5/12)/(pi/6)*.6f+.2f);
            backgroundLight.intensity = intensity;

        } else if (angle < pi*17/12) { // full night
            if (!nightLight) {
                backgroundLight.color = Color.white;
                backgroundLight.intensity = .2f;

                dayLight = false;
                nightLight = true;
            }

        } else if (angle < pi*19/12) { // transition from nightLight to dayLight
            if (angle < pi*3/2) {
                colorsGB = 1-((angle-pi*17/12)/(pi/12)*.6f);
                backgroundLight.color = new Color(1, colorsGB, colorsGB);
            } else {
                colorsGB = (angle-pi*3/2)/(pi/12)*.6f+.4f;
                backgroundLight.color = new Color(1, colorsGB, colorsGB);
            }
            
            intensity = (angle-pi*17/12)/(pi/6)*.6f+.2f;
            backgroundLight.intensity = intensity;
        }
    }
}
