using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Speed of the player movement
    public float speed = 0.01f;
    private Animator animator;
    private bool facingRight = true;
    private Vector3 velocity = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    private Rigidbody body;
    private GameObject player;
    public int maxLives = 3;
    [HideInInspector]
    public int currentLives;
    [SerializeField]
    public bool getPack;

    // Boundaries of the map
    public float minX, maxX, minY, maxY;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        body.velocity = velocity;
        ChangeHouseTags();
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

        // Clamp the player's position to stay within the map boundaries
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
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

            transform.position = new Vector3(0.0f, 0.0f, -0.0f);
            animator.ResetTrigger("run");
        }
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Shop")){
            getPack = true;
            Debug.Log(getPack);
        }
        if (other.gameObject.CompareTag("Goal")){
            getPack = false;
            Debug.Log(getPack);
        }
    }

    public void ChangeHouseTags()
    {
        // Find all GameObjects with the tag "House"
        GameObject[] houses = GameObject.FindGameObjectsWithTag("House");

        // Ensure there are at least two houses
        if (houses.Length < 2)
        {
            Debug.LogError("Not enough houses to assign tags.");
            return;
        }

        // Get two unique random indices
        int firstIndex = Random.Range(0, houses.Length);
        int secondIndex = firstIndex;
        while (secondIndex == firstIndex)
        {
            secondIndex = Random.Range(0, houses.Length);
        }

        // Change the tags of the selected houses
        houses[firstIndex].tag = "Shop";
        houses[secondIndex].tag = "Goal";

        Debug.Log($"House {houses[firstIndex].name} is now a Shop");
        Debug.Log($"House {houses[secondIndex].name} is now a Goal");
    }

    private void InitPointer()
    {
        // Find the GameObject with the tag "Shop"
        GameObject shop = GameObject.FindGameObjectWithTag("Shop");
        if (shop == null)
        {
            Debug.LogError("No GameObject with tag 'Shop' found.");
            return;
        }

        // Instantiate the pointer
        GameObject pointerPrefab = Resources.Load<GameObject>("Pointer");
        GameObject pointerInstance = Instantiate(pointerPrefab, Vector3.zero, Quaternion.identity);
        pointerInstance.tag = "ShopPointer";
    }
}