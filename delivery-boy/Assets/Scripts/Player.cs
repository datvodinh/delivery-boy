using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Speed of the player movement
    public float speed = 0.01f;
    private Animator animator;
    private bool facingRight = true;
    private Vector3 velocity = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    private Rigidbody2D body;
    private GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        body.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        // Initialize movement variables
        float moveHorizontal = 0;
        float moveVertical = 0;

        bool isMoving = false;

        // Check for "A S W D" and arrow key inputs
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -speed;
            isMoving = true;
            if (facingRight)
            {
                Flip();
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = speed;
            isMoving = true;
            if (!facingRight)
            {
                Flip();
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveVertical = speed;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveVertical = -speed;
            isMoving = true;
        }

        // Set or reset the "run" trigger based on whether the player is moving
        if (isMoving)
        {
            animator.SetTrigger("run");
        }
        else
        {
            animator.ResetTrigger("run");
        }

        // Create a Vector3 based on the input
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, -0.0f);

        // Move the player
        transform.Translate(movement * Time.deltaTime, Space.World);
    }

    // Function to flip the character
    void Flip()
    {
        // Switch the way the player is labeled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1 to flip the character
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vehicle"))
        {
            Debug.Log("Player hit by vehicle");

            transform.position = new Vector3(0.0f, 0.0f, -0.1f);
            animator.ResetTrigger("run");

        }

    }
}