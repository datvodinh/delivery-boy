using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_vehicle : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    private Rigidbody2D myBody;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        speed = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         myBody.velocity = new Vector2(-speed , myBody.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Detect Collision");
    }
}
