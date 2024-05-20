using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vertical_vehicle : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    private Rigidbody2D myBody;
    private bool canMove = true;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {

        myBody.velocity = new Vector2(myBody.velocity.x, speed);
        }
        

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vehicle"))
        {
            //Debug.Log("Detect vehicle Collision");
            //StartCoroutine(StopMoveRoutine());
            // Get the relative position of the other object compared to this object
            Vector2 hitPosition = transform.InverseTransformPoint(other.transform.position);

            // Determine which side is hit
            if (Mathf.Abs(hitPosition.x) > Mathf.Abs(hitPosition.y))
            {
                if (hitPosition.x > 0)
                {

                    Debug.Log("Hit on the right side.");//the other need to stop
                    
                    
                }

                else
                {

                    Debug.Log("Hit on the left side.");
                }
            }
            else
            {
                if (hitPosition.y > 0)
                {

                    Debug.Log("Hit on the top side.");//the object need to stop
                    StartCoroutine(StopMoveRoutine());
                    

                }
                else
                {

                    Debug.Log("Hit on the bottom side.");
                }
            }

        }

    }
    IEnumerator StopMoveRoutine()
    {
        Vector2 org = myBody.velocity;
        myBody.velocity = Vector2.zero;
        canMove = false;
        yield return new WaitForSeconds(2f);
        myBody.velocity = org;
        canMove = true;

    }

}

