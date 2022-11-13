using MWP.Enemies;
using UnityEngine;

namespace MWP.Guns.Bullets
{
    public class BulletPhysicsExplosion : BulletPhysics
    {
        [SerializeField] private LayerMask _enemyLayer;
        private int _explosionDamage;
        [SerializeField] private GameObject explosionFx;
        [Header("Explosion Variables")] 
        [SerializeField] private int _explosionRadius;
        [SerializeField] private int _explosionHitCount;
        

        private void OnDestroy()
        {
            Explode();
        }

        public void SetExplosionVariables(int explosionDamage, int explosionRadius)
        {
            _explosionDamage = explosionDamage;
            _explosionRadius = explosionRadius;
        }

        // TODO: Add Explosion VFX
        private void Explode()
        {
            var pos = gameObject.transform.position;
            var enemiesHit = Physics2D.OverlapCircleAll(pos, _explosionRadius, _enemyLayer);
            Instantiate(explosionFx, pos, Quaternion.identity);
            
            int i = 0;
            foreach (var enemyCollider in enemiesHit)
            {
                if (++i > _explosionHitCount) break;
                var enemy = enemyCollider.GetComponentInParent<Enemy>();
                if (enemy != null) DamageOnEnemy(enemy, null, _explosionDamage);
            }
        }
    }
}