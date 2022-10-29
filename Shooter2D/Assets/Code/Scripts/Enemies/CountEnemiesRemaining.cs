using MWP.GameStates;
using UnityEngine;

namespace MWP.Enemies
{
    public class CountEnemiesRemaining : MonoBehaviour
    {
        private SpawnerEnemy _parentSpawner;

        public void SetValues(SpawnerEnemy parentSpawner, Enemy enemy)
        {
            enemy.OnDeath += Count;
            _parentSpawner = parentSpawner;
        }

        private void Count()
        {
            _parentSpawner.SubtractSpawned();
            GameManager.Instance.RemainingEnemies--;
        }
    }
}