using MWP.Guns.Bullets;
using UnityEngine;

namespace MWP.Guns
{
    public abstract class GunAutomatic : Gun
    {
        //[Header("AutomaticSpecifics")]

        public override Bullet Fire(Vector2 direction, int strength, float aim)
        {
            if (!(Cd <= 0) || _curClip <= 0) return null;
            _curClip -= 1;
            return base.Fire(direction, strength, aim);

        }

        public override void ReleaseFire()
        {
        }
    }
}