using UnityEngine;
using System.Collections.Generic;
public class VehicleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] vehiclePrefabs;
    [SerializeField] Transform[] vehicleSpawnPoints;
    [SerializeField] float vehicleSpawnRate = 5f;

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

    }
}

