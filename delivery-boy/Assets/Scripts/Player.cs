using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private float rotateSpeed = 360f; // Degrees per second
    private float targetRotation = 90f; // Target rotation in degrees

    private bool canMove = true;
    [HideInInspector]
    
    private bool isRotating = false;
    private Vector3 velocity = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !isRotating)
           transform.Translate(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow) && !isRotating)
        {
            StartCoroutine(RotateRight());
        }
        if (Input.GetKey(KeyCode.RightArrow) && !isRotating)
        {
            StartCoroutine(RotateLeft());
        }
    }

    IEnumerator RotateRight()
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion targetQuaternion = transform.rotation * Quaternion.Euler(0,  0, targetRotation);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * (rotateSpeed / 90f);
            transform.rotation = Quaternion.Slerp(startRotation, targetQuaternion, t);
            yield return null;
        }

        transform.rotation = targetQuaternion;
        isRotating = false;
    }

    IEnumerator RotateLeft()
    {
        isRotating = true;
        

        Quaternion startRotation = transform.rotation;
        Quaternion targetQuaternion = transform.rotation * Quaternion.Euler(0, 0, -targetRotation);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * (rotateSpeed / 90f);
            transform.rotation = Quaternion.Slerp(startRotation, targetQuaternion, t);
            yield return null;
        }

        transform.rotation = targetQuaternion;
        isRotating = false;
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
                    
                    StartCoroutine(StopMoveRoutine());
                    
                }
            }
            else
            {
                if (hitPosition.y > 0)
                {

                    Debug.Log("Hit on the top side.");//the object need to stop


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
        Vector2 org = rb.velocity;
        rb.velocity = Vector2.zero;
        canMove = false;
        yield return new WaitForSeconds(2);
        rb.velocity = org;
        canMove = true; 

    }

}