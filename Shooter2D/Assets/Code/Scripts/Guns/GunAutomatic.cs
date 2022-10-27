using MWP.Guns.Bullets;
using UnityEngine;

namespace MWP.Guns
{
    public abstract class GunAutomatic : Gun
    {
        //[Header("AutomaticSpecifics")]

        public override Bullet Fire(Vector2 direction, int strenght, float aim)
        {
            if (Cd <= 0 && _curClip > 0)
            {
                _curClip -= 1;
                return base.Fire(direction, strenght, aim);
            }
            return null;
        }

        public override void ReleaseFire()
        {
        }
    }
}