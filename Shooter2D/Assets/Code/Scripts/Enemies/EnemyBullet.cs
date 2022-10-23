using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PhysicsBullet
{
    // TODO: Tirar depencia de bullet...
    // Overriding PhysicsBullet
    // Hack para não ter que refazer a parte de hitbox do player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null)
        {
            character.UpdateHealth(-(int)DamageCaused);
        }
        DestroyBullet();
    }

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