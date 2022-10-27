using MWP.GameStates;
using UnityEngine;

namespace MWP.Enemies
{
    public class CountEnemiesRemaining : MonoBehaviour
    {
        public SpawnerEnemy ParentSpawner
        {
            set => _parentSpawner = value;
        }

        private SpawnerEnemy _parentSpawner;

        private void OnDestroy()
        {
            _parentSpawner.SubtractSpawned();
            GameManager.Instance.RemainingEnemies--;
        }

    }
}