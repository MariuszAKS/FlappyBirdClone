using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Controller : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float delete_x_position;

    void Update()
    {
        transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;

        if (transform.position.x <= delete_x_position) {
            Destroy(gameObject);
        }
    }
}
