using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGun : Gun
{
    [Header("Explosion Variables")]
    [SerializeField] int _explosionDamage;
    [SerializeField] int _explosionRadius;

    // Garante apenas um tiro por clique do mouse
    private bool _fired = false;

    //Methods
    public override Bullet Fire(Vector2 direction, int strenght, float aim)
    {
        if (Cd <= 0 && _curClip > 0 && !_fired)
        {
            _fired = true;
            _curClip -= 1;

            PhysicsExplosionBullet bulletScript = (PhysicsExplosionBullet)base.Fire(direction, strenght, aim);
            bulletScript.SetExplosionVariables(_explosionDamage, _explosionRadius);
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
