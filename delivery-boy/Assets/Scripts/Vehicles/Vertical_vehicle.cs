using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vertical_vehicle : MonoBehaviour
{
    [HideInInspector]
    public float speed, direction;
    public bool isStopped;

    private Rigidbody myBody;
    private bool canMove = true;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        //speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {

            myBody.velocity = new Vector2(myBody.velocity.x, speed * direction);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Vehicle"))
        {
            Debug.Log("Detect vehicle Collision");
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

                    Debug.Log("Hit on the top side.");

                }
                else
                {

                    Debug.Log("Hit on the bottom side.");
                    StartCoroutine(StopMoveRoutine());

                }
            }

        }


        if (other.CompareTag("TrafficLight"))
        {
            Debug.Log("OnCollisionEnter2D called !!!!!!!!!!!!!!!!!!!!!!!!");
            Animator trafficLightAnimator = other.GetComponent<Animator>();
            if (trafficLightAnimator != null)
            {
                AnimatorStateInfo stateInfo = trafficLightAnimator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName("red") || stateInfo.IsName("green_to_red"))
                {
                    StopCar();
                    Debug.Log("STOP BY TRAFFIC LIGHT!!!");
                    StartCoroutine(CheckTrafficLightState(trafficLightAnimator));
                }
            }
        }

    }
    // For traffic light mechanism


    void StopCar()
    {
        Vector2 org = myBody.velocity;
        myBody.velocity = Vector2.zero;
        isStopped = true;
    }

    void MoveCar()
    {
        // Resume car movement; for example, set a specific velocity or add force
        Vector2 org = myBody.velocity;
        myBody.velocity = org;
        isStopped = false;
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


    IEnumerator CheckTrafficLightState(Animator trafficLightAnimator)
    {
        while (isStopped)
        {
            yield return new WaitForSeconds(0.5f);
            AnimatorStateInfo stateInfo = trafficLightAnimator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("Checking TrafficLight state: " + stateInfo.fullPathHash);

            if (stateInfo.IsName("green"))
            {
                MoveCar();
                Debug.Log("TrafficLight turned green, car moves.");
            }
        }
    }

}


