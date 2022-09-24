using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutomaticGun : Gun
{
    //[Header("AutomaticSpecifics")]

    public override void Fire(Vector2 direction, int strenght, float aim)
    {
        if (cd <= 0 && CurMagazine > 0)
        {
            base.Fire(direction, strenght, aim);
            CurMagazine -= 1;
        }
    }

    public override void ReleaseFire()
    {
    }

    // Start is called before the first frame update
    // protected virtual new void Start()
    // {
    //     base.Start();
    // }

    // // Update is called once per frame
    // protected virtual new void Update()
    // {
    //     base.Update();
    // }
}
