using MWP.Enemies;
using UnityEngine;

namespace MWP.Guns.Bullets
{
    public class BulletPhysicsExplosionSticky : BulletPhysicsExplosion
    {
        private bool _isSticked;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Stick(collision.collider.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var enemy = collision.gameObject.GetComponentInParent<Enemy>();
            if (enemy != null && !enemy.IsDead) Stick(enemy.gameObject);
        }

        private void Stick(GameObject toStick)
        {
            if (!_isSticked)
            {
                _isSticked = true;
                Destroy(_rgbd);
                gameObject.transform.parent = toStick.transform;
            }
        }
    }
}