using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy
{
    [SerializeField] Vector3 speed = new Vector3(0.1f, 0, 0);
    [SerializeField] private bool randomSpeed = false;
    protected override void Start()
    {
        base.Start();
        if (randomSpeed)
        {
            float _x = Random.Range(-0.02f, 0.02f);
            float _y = Random.Range(-0.02f, 0.02f);
            speed = new Vector3(_x, _y, 0);
        }
    }
    protected override void Movement()
    {
        transform.position += speed;
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
