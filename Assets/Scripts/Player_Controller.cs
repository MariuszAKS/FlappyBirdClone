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

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Wall")) {
            Debug.Log("Splattered all over the outer ring");
        } else if (collider.CompareTag("Ceiling")) {
            Debug.Log("Icarus flew too close to the sun");
        } else if (collider.CompareTag("Floor")) {
            Debug.Log("You're too grounded in reality");
        }

        Debug.Log("GAME OVER (to be implemented)");
    }
}
