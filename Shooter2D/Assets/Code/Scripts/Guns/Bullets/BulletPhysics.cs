using UnityEngine;

namespace MWP.Guns.Bullets
{
    public class BulletPhysics : Bullet
    {
        [SerializeField] protected Rigidbody2D _rgbd;


        public override void SetVariables(Vector2 direction, int strenght, int damage)
        {
            base.SetVariables(direction, strenght, damage);
            _rgbd.AddForce(Direction * Speed, ForceMode2D.Impulse);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Colisões com trigger não rodam essa parte
            DestroyBullet();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Enemies.Enemy enemy = collision.gameObject.GetComponentInParent<Enemies.Enemy>();
            if (enemy != null && !enemy.IsDead)
            {
                DamageOnEnemy(enemy, transform.position);
                DestroyBullet();
            }
        }
    }
}