using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertical_vehicle : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    private Rigidbody2D myBody;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        speed = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         myBody.velocity = new Vector2(myBody.velocity.x , speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Detect Collision");

    }

}
