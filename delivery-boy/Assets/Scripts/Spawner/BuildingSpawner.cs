using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] buildingPreFabs;
    [SerializeField] private GameObject shopPreFab, shippingPreFab;
    [SerializeField] private Transform[] spawnLocation;
    
    void Start()
    {
        Generate();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Generate();
        }
          
    }
    
    void Generate()
    {
        for (int i = 0; i < spawnLocation.Length; i++) if (spawnLocation[i].childCount > 0) Destroy(spawnLocation[i].GetChild(0).gameObject);

        int shopPos = Random.Range(0, spawnLocation.Length);
        GameObject shop = Instantiate(shopPreFab, spawnLocation[shopPos].position, shopPreFab.transform.rotation);
        shop.transform.SetParent(spawnLocation[shopPos]);


        int shippingPos = Random.Range(0, spawnLocation.Length);
        while(shippingPos == shopPos) shippingPos = Random.Range(0, spawnLocation.Length);
        GameObject shipping = Instantiate(shippingPreFab, spawnLocation[shippingPos].position, shippingPreFab.transform.rotation);
        shipping.transform.SetParent(spawnLocation[shippingPos]);

        for (int i = 0; i < spawnLocation.Length; i++)
        {
            if (i == shopPos || i == shippingPos) continue;

            /*
            if(spawnLocation[i].childCount > 0)
            {
                Destroy(spawnLocation[i].GetChild(0).gameObject);
            }
            */

            int ranNum = Random.Range(0, buildingPreFabs.Length);
            GameObject building = Instantiate(buildingPreFabs[ranNum], spawnLocation[i].position, buildingPreFabs[ranNum].transform.rotation);
            building.transform.SetParent(spawnLocation[i]);
        }
    }
}
