using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
