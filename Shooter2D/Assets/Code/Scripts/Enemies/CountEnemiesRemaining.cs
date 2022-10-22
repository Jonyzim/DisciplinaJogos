using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountEnemiesRemaining : MonoBehaviour
{
    public Spawner ParentSpawner
    {
        set => _parentSpawner = value;
    }

    private Spawner _parentSpawner;

    private void OnDestroy()
    {
        _parentSpawner.SubtractSpawned();
        GameManager.Instance.RemainingEnemies--;
    }

}
