using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField] float jumpVelocity;
    [SerializeField] float gravity;

    Animator anim;


    [SerializeField] AudioSource audio_WingFlap;
    [SerializeField] AudioSource audio_PointGain;
    [SerializeField] AudioSource audio_WallHit;



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb2d.velocity = new Vector2(0, jumpVelocity);
            anim.SetTrigger("Bird_WingFlap_Trigger");
            audio_WingFlap.Play();
        }

        rb2d.velocity -= new Vector2(0, gravity) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Passage")) {
            audio_PointGain.Play();
            Game_Controller.instance.IncreaseScore();
            return;
        }

        string message = "";

        if (collider.CompareTag("Wall")) {
            message = "Splattered all over the outer ring";
        } else if (collider.CompareTag("Ceiling")) {
            message = "Icarus flew too close to the sun";
        } else if (collider.CompareTag("Floor")) {
            message = "You're too grounded in reality";
        }

        audio_WallHit.Play();
        Game_Controller.instance.GameOver(message);
    }



    public void StartingPosition() {
        transform.position = new Vector3(0, 0, 0);
        rb2d.velocity = Vector2.zero;
    }
}
