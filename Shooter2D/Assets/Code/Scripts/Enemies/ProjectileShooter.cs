using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private int _bulletDamage;
    [SerializeField] private List<Transform> _spawnPos;
    [SerializeField] private List<Vector3> _direction;

    [SerializeField] private GameObject _projectilePrefab;

    public void Shoot(int x)
    {
        int i = 0;
        foreach (Transform transf in _spawnPos)
        {
            GameObject obj = Instantiate(_projectilePrefab, transf.position, Quaternion.identity);
            PhysicsBullet bullet = obj.GetComponent<PhysicsBullet>();
            if (bullet != null)
                bullet.SetVariables(_direction[i], 100, _bulletDamage);
            i++;
        }
    }
}
