using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PhysicsBullet
{
    // TODO: Tirar depencia de bullet...
    // Overriding PhysicsBullet
    private void OnCollisionEnter2D(Collision2D collision) { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.GetComponentInParent<Character>();
        if (character != null)
        {
            //print("Colidiu player");
            character.UpdateHealth(-(int)DamageCaused);
            DestroyBullet();
        }
    }
}