using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private float _spawnRate;
    [SerializeField] private int _spawnCount;
    private void SpawnRandom()
    {
        int n = _enemyPrefabs.Count;
        int randomId = Random.Range(0, n);
        Instantiate(_enemyPrefabs[randomId], transform.position, Quaternion.identity,transform);
    }

    //Unity Methods
    private void Start()
    {
        for (int i = 0; i < _spawnCount; i++)
            SpawnRandom();
        //InvokeRepeating("SpawnRandom", 0.5f, spawnRate);
    }
}
