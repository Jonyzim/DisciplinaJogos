using MWP.Enemies;
using UnityEngine;

namespace MWP.Guns.Bullets
{
    public class BulletHitscan : Bullet
    {
        [SerializeField] private float _reach;
        [SerializeField] private LayerMask _hitLayer;
        [SerializeField] protected GameObject _particles;
        private float _curLifetime;
        private Color _lineColor;


        public override void SetVariables(Vector2 direction, int strength, int damage)
        {
            base.SetVariables(direction, strength, damage);
            Vector2 position;

            var hitInfo = Physics2D.Raycast(transform.position, Direction, _reach, _hitLayer);

            if (hitInfo.collider != null)
            {
                position = hitInfo.point;

                var enemy = hitInfo.collider.gameObject.GetComponentInParent<Enemy>();
                if (enemy != null && !enemy.IsDead) DamageOnEnemy(enemy, position);
            }
            else
            {
                position = transform.position + Direction * _reach;
            }

            SpawnParticles(position);

            transform.position = new Vector3(position.x, position.y, 0);
        }

        protected virtual void SpawnParticles(Vector2 position)
        {
            _particles.transform.localScale = new Vector3(Vector3.Distance(transform.position, position), 1, 1);
            _particles.transform.SetParent(null, true);
            _particles.gameObject.transform.rotation =
                Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, Direction));

            Destroy(_particles.gameObject, 2);
        }
    }
}