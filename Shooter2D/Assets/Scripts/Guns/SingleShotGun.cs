using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleShotGun : Gun
{
    //[Header("SingleShotSpecifics")]
    private bool _fired = false;

    //Methods
    public override void Fire(Vector2 direction, int strenght, float aim)
    {
        if (cd <= 0 && CurMagazine > 0 && !_fired)
        {
            base.Fire(direction, strenght, aim);
            
            _fired = true;
            CurMagazine -= 1;
        }

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
