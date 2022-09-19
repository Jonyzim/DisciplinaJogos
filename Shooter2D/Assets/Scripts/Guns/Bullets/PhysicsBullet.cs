using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class PhysicsBullet : Bullet
{
    [SerializeField] private Rigidbody2D rgbd;


    public override void SetDirection(Vector3 _direction){
        base.SetDirection(_direction);
        rgbd.AddForce(_direction * speed, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Colisões com trigger não rodam essa parte
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null){
            enemy.Damage(transform.position, damageCaused);
            DestroyBullet();
        }
    }
}
