using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSpawner : VehicleSpawner
{
    [SerializeField]
    private GameObject[] vehiclesReference;
    private GameObject spawnedVehicles;

    [SerializeField]
    private Transform pos;

    private int randomIndex;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnVehicles());
    }

    IEnumerator SpawnVehicles()
    {
        while (true)
        {


            yield return new WaitForSeconds(Random.Range(1, 5));

            for (int i = 0; i < carsPerSpawn; i++)
            {
                randomIndex = Random.Range(0, vehiclesReference.Length);

                spawnedVehicles = Instantiate(vehiclesReference[randomIndex]);

                spawnedVehicles.transform.position = pos.position;

                Transform my_transform = spawnedVehicles.transform;
                my_transform.localScale = new Vector3(-my_transform.localScale.x,my_transform.localScale.y,my_transform.localScale.z);

                spawnedVehicles.GetComponent<Horizontal_vehicle>().speed = carSpeed;
                spawnedVehicles.GetComponent<Horizontal_vehicle>().direction = -1;

                yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            }

        }
    }
}
