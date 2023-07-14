using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Background_Controller : MonoBehaviour
{
    const float pi = Mathf.PI;

    [SerializeField] GameObject[] Clouds;
    [SerializeField] GameObject[] Background;
    [SerializeField] GameObject[] Foreground;
    [SerializeField] GameObject Sky, Sun, Moon;
    float angleSun = 0, angleMoon = pi;
    Vector2 celestialAnchor = new Vector2(0, -3);
    float celestialRadius = 6.5f;

    [SerializeField] float speedSun;
    [SerializeField] float speedMoon;
    [SerializeField] float speedClouds;
    [SerializeField] float speedBackground;
    [SerializeField] float speedForeground;

    [SerializeField] Light2D lightBackground;
    [SerializeField] Light2D lightSun, lightMoon;

    [SerializeField] bool isDayStart, isDayFull = true, isDayEnd, isNightStart, isNightFull, isNightEnd;

    [SerializeField] Color colorSunrise;
    [SerializeField] float intensitySunrise;
    [SerializeField] Color colorDaylight;
    [SerializeField] float intensityDaylight;
    [SerializeField] Color colorSunset;
    [SerializeField] float intensitySunset;
    [SerializeField] Color colorNightlight;
    [SerializeField] float intensityNightlight;
    Color colorChange = new Color(0, 0, 0, 0);
    float intensityChange = 0;

    public void ResetBackground() {
        angleSun = 0; angleMoon = pi;
        isDayStart = isDayEnd = false;
        isDayFull = true;
        isNightStart = isNightFull = isNightEnd = false;
        colorChange = new Color(0, 0, 0, 0);
        intensityChange = 0;
        CorrectColorAndIntensity(colorDaylight, intensityDaylight);
    }
    
    void FixedUpdate()
    {
        MoveCelestial(ref Sun, ref angleSun, speedSun);
        MoveCelestial(ref Moon, ref angleMoon, speedMoon);
        MovePartOfBackground(ref Clouds, speedClouds);
        MovePartOfBackground(ref Background, speedBackground);
        MovePartOfBackground(ref Foreground, speedForeground);

        UpdateLighting(angleSun);

        lightSun.intensity = angleSun > pi/2 && angleSun < pi*3/2 ? 0 : .3f;
        lightMoon.intensity = angleMoon > pi/2 && angleMoon < pi*3/2 ? 0 : .3f;
    }

    void MovePartOfBackground(ref GameObject[] parts, float speed) {
        Vector3 frameMove = Vector3.left * speed * Time.fixedDeltaTime;

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
        angle += speed * Time.fixedDeltaTime;
        if (angle >= 2 * pi) {
            angle -= 2 * pi;
        }

        float xPos = celestialAnchor.x + celestialRadius * Mathf.Sin(angle);
        float yPos = celestialAnchor.y + celestialRadius * Mathf.Cos(angle);

        celestial.transform.position = new Vector3(xPos, yPos, 0);
    }

    void UpdateLighting(float angle) {
        if (isDayStart) {
            if (angleSun > pi*19/12) {
                isDayStart = false;
                isDayFull = true;
                CorrectColorAndIntensity(colorDaylight, intensityDaylight);
            }
            else UpdateColorAndIntensity();
        }
        else if (isDayFull) {
            if (angleSun > pi*5/12 && angleSun < pi) {
                isDayFull = false;
                isDayEnd = true;
                UpdateColorAndIntensityChange(colorSunset, intensitySunset);
            }
        }
        else if (isDayEnd) {
            if (angleSun > pi/2) {
                isDayEnd = false;
                isNightStart = true;
                CorrectColorAndIntensity(colorSunset, intensitySunset);
                UpdateColorAndIntensityChange(colorNightlight, intensityNightlight);
            }
            else UpdateColorAndIntensity();
        }

        else if (isNightStart) {
            if (angleSun > pi*7/12) {
                isNightStart = false;
                isNightFull = true;
                CorrectColorAndIntensity(colorNightlight, intensityNightlight);
            }
            else UpdateColorAndIntensity();
        }
        else if (isNightFull) {
            if (angleSun > pi*17/12) {
                isNightFull = false;
                isNightEnd = true;
                UpdateColorAndIntensityChange(colorSunrise, intensitySunrise);
            }
        }
        else if (isNightEnd) {
            if (angleSun > pi*3/2) {
                isNightEnd = false;
                isDayStart = true;
                CorrectColorAndIntensity(colorSunrise, intensitySunrise);
                UpdateColorAndIntensityChange(colorDaylight, intensityDaylight);
            }
            else UpdateColorAndIntensity();
        }
    }

    void UpdateColorAndIntensityChange(Color targetColor, float targetIntensity) {
        int framesToChangeState = CalculateFramesToChangeState(speedSun, pi/12);

        colorChange = CalculateColorChange(lightBackground.color, targetColor, framesToChangeState);
        intensityChange = CalculateIntensityChange(lightBackground.intensity, targetIntensity, framesToChangeState);
    }

    int CalculateFramesToChangeState(float speed, float angleDifference) {
        float angleDifferenceSingleFrame = speed * Time.fixedDeltaTime;
        int framesToChangeState = Mathf.FloorToInt(angleDifference / angleDifferenceSingleFrame);
        
        return framesToChangeState;
    }

    Color CalculateColorChange(Color colorStart, Color colorEnd, int framesToChangeState) {
        Color colorDifference = colorEnd - colorStart;
        Color colorDifferenceSingleFrame = colorDifference / (float)framesToChangeState;
        
        return colorDifferenceSingleFrame;
    }

    float CalculateIntensityChange(float intensityStart, float intensityEnd, int framesToChangeState) {
        float intensityDifference = intensityEnd - intensityStart;
        float intensityDifferenceSingleFrame = intensityDifference / (float)framesToChangeState;
        
        return intensityDifferenceSingleFrame;
    }

    void UpdateColorAndIntensity() {
        lightBackground.color += colorChange;
        lightBackground.intensity += intensityChange;
    }

    void CorrectColorAndIntensity(Color color, float intensity) {
        lightBackground.color = color;
        lightBackground.intensity = intensity;
    }
}
