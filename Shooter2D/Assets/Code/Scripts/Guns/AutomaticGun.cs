using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutomaticGun : Gun
{
    //[Header("AutomaticSpecifics")]

    public override void Fire(Vector2 direction, int strenght, float aim)
    {
        if (Cd <= 0 && CurClip > 0)
        {
            CurClip -= 1;
            base.Fire(direction, strenght, aim);
        }
    }

    public override void ReleaseFire()
    {
    }
}
