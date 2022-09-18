using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy
{
    [SerializeField] Vector3 speed = new Vector3(0.1f,0,0);
    protected override void Movement()
    {
        transform.position += speed;
    } 

    public override void Damage(Vector3 hitPos)
    {
        //print("DAMAGE");
        base.Damage(hitPos);
        StartCoroutine(DamageFx());
    }
    void Update()
    {
        Movement();
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Wall"))
        {
            speed = -speed;
        }
    }

}
