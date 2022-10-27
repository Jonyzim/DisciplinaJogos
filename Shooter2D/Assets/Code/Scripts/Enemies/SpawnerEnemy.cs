using MWP.GameStates;
using System.Collections.Generic;
using UnityEngine;

namespace MWP.Enemies
{
    public class SpawnerEnemy : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _enemyPrefabs;

        public LayerMask SpawnLayer;

        private List<Vector3> _spawnPoints;

        [SerializeField] private float _spawnRadius;

        [SerializeField] private float _spawnDelay;

        [SerializeField] private int _maxConcurrentSpawns;

        private int _curSpawned;
        private float _spawnTimer;

        public void SubtractSpawned()
        {
            _curSpawned--;
        }

        private void SpawnRandom()
        {
            if (GameManager.Instance.RemainingEnemies - _curSpawned > 0)
            {

                if (_curSpawned < _maxConcurrentSpawns)
                {
                    _spawnPoints = GetSpawnPoints();
                    if (_spawnPoints.Count == 0)
                        return;

                    int nEnemy = _enemyPrefabs.Count;
                    int randomId = Random.Range(0, nEnemy);

                    int nPos = _spawnPoints.Count;
                    int randomPos = Random.Range(0, nPos);

                    GameObject enemy = Instantiate(_enemyPrefabs[randomId], _spawnPoints[randomPos], Quaternion.identity, transform);

                    CountEnemiesRemaining count = enemy.AddComponent<CountEnemiesRemaining>();
                    count.ParentSpawner = this;

                    _curSpawned++;
                }
            }
        }

        private List<Vector3> GetSpawnPoints()
        {
            List<Vector3> spawnPoints = new List<Vector3>();
            Vector3 camPos = Camera.main.transform.position;
            Collider2D[] spawnsHit = Physics2D.OverlapCircleAll(camPos, _spawnRadius, SpawnLayer);

            foreach (Collider2D spawnCollider in spawnsHit)
            {
                Vector3 positionViewport = Camera.main.WorldToViewportPoint(spawnCollider.transform.position);


                // If not inside camera view
                if (!(positionViewport.x >= 0 && positionViewport.y >= 0 && positionViewport.x <= 1 && positionViewport.y <= 1))
                    spawnPoints.Add(spawnCollider.transform.position);
            }

            return spawnPoints;
        }



        //Unity Methods
        private void Start()
        {
            _curSpawned = 0;
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
    }
}