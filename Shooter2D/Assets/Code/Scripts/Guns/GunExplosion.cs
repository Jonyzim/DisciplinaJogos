using MWP.Guns.Bullets;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Guns
{
    public class GunExplosion : Gun
    {
        [FormerlySerializedAs("_explosionDamage")] [Header("Explosion Variables")] [SerializeField]
        private int explosionDamage;

        [FormerlySerializedAs("_explosionRadius")] [SerializeField] private int explosionRadius;

        // Garante apenas um tiro por clique do mouse
        private bool _fired;

        //Methods
        public override Bullet Fire(Vector2 direction, int strength, float aim)
        {
            if (Cd <= 0 && _curClip > 0 && !_fired)
            {
                _fired = true;
                _curClip -= 1;

                var bulletScript = (BulletPhysicsExplosion)base.Fire(direction, strength, aim);
                bulletScript.SetExplosionVariables(explosionDamage, explosionRadius);
                return bulletScript;
            }

            return null;
        }

        public override void ReleaseFire()
        {
            _fired = false;
        }

        protected override void ReloadProps(float time)
        {
        }
    }
}