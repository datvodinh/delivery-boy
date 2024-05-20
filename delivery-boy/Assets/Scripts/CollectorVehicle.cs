using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorVehicle : MonoBehaviour
{
    private string VEHICLE = "Vehicle";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(VEHICLE))
        {
            Destroy(collision.gameObject);
        }
    }
}
