using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerScript : MonoBehaviour
{
    public List<GameObject> cars = new List<GameObject>();

    private void Start()
    {
        float spawnTime = Random.Range(2, 5);
        InvokeRepeating("SpawnCar", 0, spawnTime);
    }

    private void SpawnCar()
    {
        foreach (var car in cars)
        {
            if (car.activeSelf == false)
            {
                car.SetActive(true);
                car.transform.position = transform.position;
                break;
            }
        }
    }
}
