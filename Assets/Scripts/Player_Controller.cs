using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField] float jumpVelocity;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb2d.velocity = new Vector2(0, jumpVelocity);
        }
    }
}
