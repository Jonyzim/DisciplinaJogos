using System.Collections.Generic;
using MWP.Guns.Bullets;
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
            var i = 0;
            foreach (var transf in _spawnPos)
            {
                var obj = Instantiate(_projectilePrefab, transf.position, Quaternion.identity);
                var bullet = obj.GetComponent<BulletPhysics>();
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