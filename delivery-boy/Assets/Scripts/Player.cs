using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public int currentLives = 3;
    [SerializeField]
    public bool getPack = false;
    [SerializeField]
    public GameObject pointer, buildingPointer;
    public TextMeshProUGUI pressFtext;

    // Boundaries of the map
    public float minX, maxX, minY, maxY;
    public GameObject shop, goal;
    private int number_of_packet = 1;
    private bool textIsFlickering = false;

    private bool nearShop = false;
    private bool nearGoal = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        body.velocity = velocity;
        pressFtext.gameObject.SetActive(false);
        ChangeHouseTags();
    }

    // Update is called once per frame
    void Update()
    {
        // Initialize movement variables
        float moveHorizontal = 0;
        float moveVertical = 0;
        Vector3 offset = new Vector3(0, 1, 0);
        if (pointer != null)
        {
            pointer.transform.position = transform.position + offset;
        }

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

        if (!getPack)
        {
            buildingPointer.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            buildingPointer.GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (nearShop && Input.GetKey(KeyCode.Space) && !getPack)
        {
            HandleShopInteraction();
        }
        else if (nearGoal && Input.GetKey(KeyCode.Space) && getPack)
        {
            HandleGoalInteraction();
        }


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shop"))
        {
            Debug.Log("Player entered shop");
            nearShop = true;
            if (!getPack)
            {
                pressFtext.gameObject.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Player entered goal");
            nearGoal = true;
            if (getPack)
            {
                pressFtext.gameObject.SetActive(true);
            }
        }
        else if (other.CompareTag("Vehicle"))
        {
            Debug.Log("Player hit by vehicle");

            transform.position = new Vector3(-7.0f, -10.0f, -0.0f);
            animator.ResetTrigger("run");
            currentLives -= 1;
            GameObject[] hearts = GameObject.FindGameObjectsWithTag("Heart");
            Destroy(hearts[0]);
            if (currentLives == 0)
            {
                Debug.Log("Game Over");
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Shop"))
        {
            nearShop = false;
            pressFtext.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            nearGoal = false;
            pressFtext.gameObject.SetActive(false);
        }
    }

    private void HandleShopInteraction()
    {
        Debug.Log("Yellow");
        pressFtext.gameObject.SetActive(false);
        getPack = true;
        pointer.GetComponent<TargetIndicator>().Target = goal;
        buildingPointer.transform.position = goal.transform.position + new Vector3(0, 2, 0);
    }

    private void HandleGoalInteraction()
    {
        Debug.Log("Green");
        getPack = false;
        pressFtext.gameObject.SetActive(false);
        pointer.GetComponent<TargetIndicator>().Target = shop;
        buildingPointer.transform.position = shop.transform.position + new Vector3(0, 2, 0);
        number_of_packet -= 1;
        Debug.Log($"number_of_packet: {number_of_packet}");
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
        int firstIndex = UnityEngine.Random.Range(0, houses.Length);
        int secondIndex = firstIndex;
        while (secondIndex == firstIndex)
        {
            secondIndex = UnityEngine.Random.Range(0, houses.Length);
        }

        // Change the tags of the selected houses
        houses[firstIndex].tag = "Shop";
        houses[secondIndex].tag = "Goal";
        shop = houses[firstIndex];
        goal = houses[secondIndex];
        if (!getPack)
        {
            // Debug.Log("now, shop");
            pointer.GetComponent<TargetIndicator>().Target = shop;
            buildingPointer.transform.position = shop.transform.position + new Vector3(0, 2, 0);
            buildingPointer.GetComponent<SpriteRenderer>().color = Color.yellow;
        }

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