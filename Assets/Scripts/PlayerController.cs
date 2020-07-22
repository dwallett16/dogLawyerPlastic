using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;

    new private Rigidbody2D rigidbody2D;
    private Animator animator;
    private float currentDirection;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (transform.rotation.y == 0) currentDirection = 1f; else currentDirection = -1f;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxisRaw ("Horizontal");

        if (moveHorizontal != 0) {
            animator.SetBool("Walk", true);
        }
        else {
            animator.SetBool("Walk", false);
        }

        if (currentDirection != moveHorizontal && moveHorizontal != 0) {
            currentDirection = moveHorizontal;
            transform.Rotate(0f, 180f, 0f, Space.Self);
        }

        if (moveHorizontal == 0) {
            if (Input.GetButton("Smoke")) animator.SetBool("IsSmoking", true); else animator.SetBool("IsSmoking", false);
        }

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2 (moveHorizontal * speed, 0);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rigidbody2D.velocity = movement;
    }
}