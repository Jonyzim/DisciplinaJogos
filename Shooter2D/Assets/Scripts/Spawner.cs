using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] float spawnRate;
    [SerializeField] int spawnCount;
    private void Start()
    {
        for (int i = 0; i < spawnCount; i++)
            SpawnRandom();
        //InvokeRepeating("SpawnRandom", 0.5f, spawnRate);
    }
    private void SpawnRandom()
    {
        int n = enemyPrefabs.Count;
        int randomId = Random.Range(0, n);
        Instantiate(enemyPrefabs[randomId], transform.position, Quaternion.identity);
    }
}
