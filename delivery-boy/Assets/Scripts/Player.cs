using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private float rotateSpeed = 360f; // Degrees per second
    private float targetRotation = 90f; // Target rotation in degrees

    private bool isRotating = false;
    private Vector3 velocity = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = velocity;
    }

    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.A) && !isRotating)
        {
            StartCoroutine(RotateRight());
        }
        if (Input.GetKeyDown(KeyCode.D) && !isRotating)
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

}