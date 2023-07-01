using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField] float jumpVelocity;
    [SerializeField] float gravity;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Game_Controller.instance.gameRunning) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                rb2d.velocity = new Vector2(0, jumpVelocity);
            }

            rb2d.velocity -= new Vector2(0, gravity) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        string message = "";

        if (collider.CompareTag("Wall")) {
            message = "Splattered all over the outer ring";
        } else if (collider.CompareTag("Ceiling")) {
            message = "Icarus flew too close to the sun";
        } else if (collider.CompareTag("Floor")) {
            message = "You're too grounded in reality";
        }

        Game_Controller.instance.GameOver(message);
    }
}
