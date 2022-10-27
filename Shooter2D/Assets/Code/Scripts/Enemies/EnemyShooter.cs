using MWP.Guns.Bullets;
using System.Collections.Generic;
using UnityEngine;

namespace MWP.Enemies
{
    public class EnemyShooter : Enemy
    {
        [SerializeField] private int _bulletDamage;
        [SerializeField] private List<Transform> _spawnPos;
        [SerializeField] private List<Vector3> _direction;

        [SerializeField] private GameObject _projectilePrefab;

        public void Shoot()
        {
            int i = 0;
            foreach (Transform transf in _spawnPos)
            {
                GameObject obj = Instantiate(_projectilePrefab, transf.position, Quaternion.identity);
                BulletPhysics bullet = obj.GetComponent<BulletPhysics>();
                if (bullet != null)
                    bullet.SetVariables(_direction[i], 100, _bulletDamage);
                i++;
            }
        }

        public override void Attack()
        {
            // int i = 0;
            // foreach (Transform transf in _spawnPos)
            // {
            //     GameObject obj = Instantiate(_projectilePrefab, transf.position, Quaternion.identity);
            //     PhysicsBullet bullet = obj.GetComponent<PhysicsBullet>();
            //     if (bullet != null)
            //         bullet.SetVariables(_direction[i], 100, _bulletDamage);
            //     i++;
            // }
        }
    }
}