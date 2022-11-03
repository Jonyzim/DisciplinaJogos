using UnityEngine;

namespace MWP.Guns.Bullets
{
    public class BulletLaser : BulletHitscan
    {
        protected override void SpawnParticles(Vector2 position)
        {
            GunLaser gunLaser = (GunLaser)_gun;
            gunLaser.SetLinePos(1, (Vector3)position);
        }
    }
}