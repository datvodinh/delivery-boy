using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorVehicle : MonoBehaviour
{
    private string VEHICLE = "Vehicle";
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(VEHICLE))
        {
            Destroy(collision.gameObject);
        } 
    }
}
