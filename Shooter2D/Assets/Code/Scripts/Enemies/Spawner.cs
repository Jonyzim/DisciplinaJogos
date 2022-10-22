using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private float _spawnRate;

    [SerializeField] private int _maxConcurrentSpawns;
    private int _curSpawned;

    public void SubtractSpawned()
    {
        _curSpawned--;
    }

    private void SpawnRandom()
    {
        if ((GameManager.Instance.RemainingEnemies - _curSpawned) > 0)
        {
            if (_curSpawned < _maxConcurrentSpawns)
            {
                _curSpawned++;

                int n = _enemyPrefabs.Count;
                int randomId = Random.Range(0, n);
                GameObject enemy = Instantiate(_enemyPrefabs[randomId], transform.position, Quaternion.identity, transform);

                CountEnemiesRemaining count = enemy.AddComponent<CountEnemiesRemaining>();
                count.ParentSpawner = this;
            }
        }
    }



    //Unity Methods
    private void Start()
    {
        _curSpawned = 0;
    }

    private void Update()
    {
        SpawnRandom();
    }
}
