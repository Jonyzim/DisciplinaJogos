using MWP.Guns.Bullets;
using UnityEngine;

namespace MWP.Enemies
{
    public class BulletEnemy : BulletPhysics
    {
        // TODO: Tirar dependencia de bullet...
        // Overriding PhysicsBullet
        // Hack para não ter que refazer a parte de hitbox do player
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var character = collision.gameObject.GetComponent<Character.Character>();
            if (character != null) character.UpdateHealth(-(int)DamageCaused);
            DestroyBullet();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var character = collision.GetComponentInParent<Character.Character>();
            
            if (character == null) return;
            character.UpdateHealth(-(int)DamageCaused);
            DestroyBullet();
        }
    }
}