using MWP.Enemies;
using UnityEngine;

namespace MWP.Guns.Bullets
{
    public class BulletPhysics : Bullet
    {
        [SerializeField] protected Rigidbody2D _rgbd;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Colisões com trigger não rodam essa parte
            DestroyBullet();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var enemy = collision.gameObject.GetComponentInParent<Enemy>();
            if (enemy != null && !enemy.IsDead)
            {
                DamageOnEnemy(enemy, transform.position);
                DestroyBullet();
            }
        }


        public override void SetVariables(Vector2 direction, int strength, int damage)
        {
            base.SetVariables(direction, strength, damage);
            _rgbd.AddForce(Direction * Speed, ForceMode2D.Impulse);
        }
    }
}