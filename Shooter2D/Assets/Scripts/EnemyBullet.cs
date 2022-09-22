using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PhysicsBullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character player = collision.gameObject.GetComponent<Character>();
        if (player != null)
        {
            print("Colidiu player");
            DestroyBullet();
        }
    }
}