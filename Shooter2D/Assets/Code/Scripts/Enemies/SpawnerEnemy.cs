using System.Collections.Generic;
using MWP.GameStates;
using UnityEngine;

namespace MWP.Enemies
{
    public class SpawnerEnemy : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _enemyPrefabs;

        public LayerMask SpawnLayer;

        [SerializeField] private float _spawnRadius;

        [SerializeField] private float _spawnDelay;

        [SerializeField] private int _maxConcurrentSpawns;

        private int _curSpawned;

        private List<Vector3> _spawnPoints;
        private float _spawnTimer;
        private Camera _camera;


        //Unity Methods
        private void Start()
        {
            _camera = Camera.main;
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

        public void SubtractSpawned()
        {
            _curSpawned--;
        }

        private void SpawnRandom()
        {
            if (GameManager.Instance.RemainingEnemies - _curSpawned > 0)
                if (_curSpawned < _maxConcurrentSpawns)
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
    }
}