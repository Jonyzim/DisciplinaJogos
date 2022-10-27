using MWP.Guns.Bullets;
using UnityEngine;

namespace MWP.Guns
{
    public abstract class GunSingleShot : Gun
    {
        // Garante apenas um tiro por clique do mouse
        private bool _fired = false;

        //Methods
        public override Bullet Fire(Vector2 direction, int strenght, float aim)
        {
            if (Cd <= 0 && _curClip > 0 && !_fired)
            {
                _fired = true;
                _curClip -= 1;

                return base.Fire(direction, strenght, aim);
            }
            return null;

        }
        public override void ReleaseFire()
        {
            _fired = false;
        }

        // Start is called before the first frame update
        // protected virtual new void Start()
        // {
        //     base.Start();
        // }

        // Update is called once per frame
        // protected virtual new void Update()
        // {
        //     base.Update();
        // }
    }
}