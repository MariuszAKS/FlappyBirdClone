using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Controller : MonoBehaviour
{
    [SerializeField] float cloudsSpeed;
    [SerializeField] float backgroundSpeed;
    [SerializeField] float foregroundSpeed;

    GameObject Sky;
    GameObject[] Clouds;
    GameObject[] Background;
    GameObject[] Foreground;
    
    void Start()
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
    }

    void Update()
    {
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
}
