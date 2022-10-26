using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsExplosionBullet : PhysicsBullet
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
            Enemy enemy = enemyCollider.GetComponentInParent<Enemy>();
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
