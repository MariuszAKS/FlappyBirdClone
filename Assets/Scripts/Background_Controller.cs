using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Controller : MonoBehaviour
{
    [SerializeField] float backgroundSpeed;
    GameObject Image_1, Image_2;
    bool image_1_onTheLeft;
    
    void Start()
    {
        Image_1 = transform.Find("Image_1").gameObject;
        Image_2 = transform.Find("Image_2").gameObject;
        image_1_onTheLeft = true;
    }

    void Update()
    {
        Vector3 movement = Vector3.left * backgroundSpeed * Time.deltaTime;
        Image_1.transform.localPosition += movement;
        Image_2.transform.localPosition += movement;

        if (Image_1.transform.localPosition.x <= -16 || Image_2.transform.localPosition.x <= -16) {
            if (image_1_onTheLeft) {
                Image_1.transform.localPosition += Vector3.right * 32;
            } else {
                Image_2.transform.localPosition += Vector3.right * 32;
            }
            
            image_1_onTheLeft = !image_1_onTheLeft;
        }
    }
}
