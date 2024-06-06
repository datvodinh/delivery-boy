using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Speed of the player movement
    public float speed = 0.01f;
    private Animator animator;
    private bool facingRight = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Initialize movement variables
        float moveHorizontal = 0;
        float moveVertical = 0;

        // Check for "A S W D" and arrow key inputs
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -speed;
            animator.SetTrigger("run");
            if (facingRight)
            {
                Flip();
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = speed;
            animator.SetTrigger("run");
            if (!facingRight)
            {
                Flip();
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveVertical = speed;
            animator.SetTrigger("run");
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveVertical = -speed;
            animator.SetTrigger("run");
        }

        // Create a Vector3 based on the input
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

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
}