using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Controller : MonoBehaviour
{
    const float pi = Mathf.PI;

    [SerializeField] float sunSpeed;
    [SerializeField] float moonSpeed;
    [SerializeField] float cloudsSpeed;
    [SerializeField] float backgroundSpeed;
    [SerializeField] float foregroundSpeed;

    GameObject Sun;
    GameObject Moon;
    float sunAngle;
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
        sunAngle = 1.5f * pi;
        moonAngle = 0.5f * pi;
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
        Debug.Log($"{xPos} {yPos}");

        celestial.transform.position = new Vector3(xPos, yPos, 0);
    }
}
