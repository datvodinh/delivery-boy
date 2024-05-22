using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSpawner : VehicleSpawner
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
                Vector3 new_scale = new Vector3(1f, -1f, 1f);
                spawnedVehicles.transform.localScale = new_scale;
                spawnedVehicles.GetComponent<Vertical_vehicle>().speed = carSpeed;
                spawnedVehicles.GetComponent<Vertical_vehicle>().direction = -1;
                yield return new WaitForSeconds(Random.Range(minTime , maxTime));
            }

        }
    }
}
