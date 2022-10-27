using UnityEngine;

namespace MWP.Guns.Bullets
{
    public class BulletPhysicsExplosion : BulletPhysics
    {
        [Header("Explosion Variables")]
        private int _explosionRadius;
        private int _explosionDamage;
        [SerializeField] private LayerMask _enemyLayer;

        public void SetExplosionVariables(int explosionDamage, int explosionRadius)
        {
            _explosionDamage = explosionDamage;
            _explosionRadius = explosionRadius;
        }

        // TODO: Add Explosion VFX
        private void Explode()
        {
            Vector3 pos = gameObject.transform.position;
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(pos, _explosionRadius, _enemyLayer);

            foreach (Collider2D enemyCollider in enemiesHit)
            {
                Enemies.Enemy enemy = enemyCollider.GetComponentInParent<Enemies.Enemy>();
                if (enemy != null)
                {
                    DamageOnEnemy(enemy, null, _explosionDamage);
                }
            }
        }

        private void OnDestroy()
        {
            Explode();
        }

    }
}