using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PhysicsBullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null)
        {
            print("Colidiu player");
            character.GetDamage((int)DamageCaused);
            DestroyBullet();
        }
    }
}