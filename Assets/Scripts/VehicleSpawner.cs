using UnityEngine;
using System.Collections.Generic;
public class VehicleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] vehiclePrefabs;
    [SerializeField] Transform[] vehicleSpawnPoints;
    [SerializeField] float vehicleSpawnRate = 5f;
    [SerializeField] GameObject txt_TruckKun;
    [Range(1, 100)]
    public int TruckKunSpawnChance = 10; //Percentage from 1 to 100 as a whole number
    private void Start()
    {
        InvokeRepeating(nameof(SpawnCar), 0.5f, vehicleSpawnRate);
    }


    void SpawnCar()
    {
        Debug.Log("Spawning Car");
        int RandomVehicleIndex = Random.Range(0, vehiclePrefabs.Length);
        int RandomSpawnPointIndex = Random.Range(0, vehicleSpawnPoints.Length);
        GameObject carObject = Instantiate(vehiclePrefabs[RandomVehicleIndex], vehicleSpawnPoints[RandomSpawnPointIndex].position, Quaternion.identity, transform);
        if(Random.Range(1, 100) <= TruckKunSpawnChance)
        {
            Debug.Log("Spawning TruckKun");
            Instantiate(txt_TruckKun, carObject.transform);
            //carObject.transform.localScale = Vector3.one * 4;
        }
    }
}

