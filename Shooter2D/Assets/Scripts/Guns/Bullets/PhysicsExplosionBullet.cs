using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsExplosionBullet : Bullet
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private Rigidbody2D _rgbd;

    public override void SetVariables(Vector2 direction, int strenght, int damage)
    {
        base.SetVariables(direction, strenght, damage);
        _rgbd.AddForce(Direction * Speed, ForceMode2D.Impulse);
    }


    // TODO: Add Explosion
    private void Explode()
    {
        Vector3 pos = gameObject.transform.position;
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(pos, _explosionRadius);

        foreach (Collider2D enemyCollider in enemiesHit)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                DamageOnEnemy(enemy, pos);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null && !enemy.IsDead)
        {
            Explode();
            DestroyBullet();
        }
    }

}
