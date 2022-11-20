using System;
using System.Collections.Generic;
using System.Linq;
using MWP.GameStates;
using MWP.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MWP.Enemies
{
    [DisallowMultipleComponent]
    public class SpawnerEnemy : MonoBehaviour
    {
        private List<GameObject> _enemyPrefabs;

        [SerializeField] private List<EnemyToSpawn> enemiesToSpawn;

        public LayerMask SpawnLayer;

        [SerializeField] private float _spawnRadius;

        [SerializeField] private float _spawnDelay;
    
        [Header("Concurrent Spawn Parameters")]
        [SerializeField] private int baseConcurrentSpawns;
        [SerializeField] private int maxConcurrentSpawns;
        [SerializeField] private int scalingConcurrentSpawns;

        private int _curSpawned;

        private List<Vector3> _spawnPoints;
        private float _spawnTimer;
        private Camera _camera;


        private void Awake()
        {
            _enemyPrefabs = new List<GameObject>();
        }

        //Unity Methods
        private void Start()
        {
            
            _camera = Camera.main;
            _curSpawned = 0;
            GameEvents.Instance.OnWaveBegin += UpdateEnemyList;
            GameEvents.Instance.OnWaveEnd += UpdateConcurrentSpawns;
        }

        private void UpdateConcurrentSpawns()
        {
            baseConcurrentSpawns += scalingConcurrentSpawns;
            if (baseConcurrentSpawns >= maxConcurrentSpawns)
            {
                baseConcurrentSpawns = maxConcurrentSpawns;
            }
        }

        private void Update()
        {
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer < 0)
            {
                SpawnRandom();
                _spawnTimer = _spawnDelay;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            if (Camera.main != null)
                Gizmos.DrawWireSphere(Camera.main.transform.position, _spawnRadius);
        }

        public void SubtractSpawned()
        {
            _curSpawned--;
        }

        private void SpawnRandom()
        {
            if (GameManager.Instance.RemainingEnemies - _curSpawned <= 0) return;
            
            if (_curSpawned < baseConcurrentSpawns)
            {
                _spawnPoints = GetSpawnPoints();
                if (_spawnPoints.Count == 0)
                    return;

                var nEnemy = _enemyPrefabs.Count;
                var randomId = Random.Range(0, nEnemy);

                var nPos = _spawnPoints.Count;
                var randomPos = Random.Range(0, nPos);

                var enemy = Instantiate(_enemyPrefabs[randomId], _spawnPoints[randomPos], Quaternion.identity,
                    transform);

                var count = enemy.AddComponent<CountEnemiesRemaining>();
                count.SetValues(this, enemy.GetComponent<Enemy>());

                _curSpawned++;
            }
        }

        private List<Vector3> GetSpawnPoints()
        {
            var spawnPoints = new List<Vector3>();
            var camPos = _camera.transform.position;
            var spawnsHit = Physics2D.OverlapCircleAll(camPos, _spawnRadius, SpawnLayer);

            foreach (var spawnCollider in spawnsHit)
            {
                var positionViewport = _camera.WorldToViewportPoint(spawnCollider.transform.position);


                // If not inside camera view
                if (!(positionViewport.x >= 0 && positionViewport.y >= 0 && positionViewport.x <= 1 &&
                      positionViewport.y <= 1))
                    spawnPoints.Add(spawnCollider.transform.position);
            }

            return spawnPoints;
        }

        private void UpdateEnemyList()
        {
            var removeList = new List<EnemyToSpawn>();
            foreach (var enemyToSpawn in enemiesToSpawn)
            {
                if (enemyToSpawn.spawnWave != GameManager.Instance.CurWave) continue;
                
                _enemyPrefabs.Add(enemyToSpawn.enemyPrefab);
                removeList.Add(enemyToSpawn);
            }
            
            foreach(var enemyToRemove in removeList)
            {
                enemiesToSpawn.Remove(enemyToRemove);
            }
            
        }
        
        [Serializable]
        private struct EnemyToSpawn
        {
            public GameObject enemyPrefab;
            public int spawnWave;
        }
    }
}