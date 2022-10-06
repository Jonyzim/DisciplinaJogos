using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleShotGun : Gun
{
    // Garante apenas um tiro por clique do mouse
    private bool _fired = false;

    //Methods
    public override void Fire(Vector2 direction, int strenght, float aim)
    {
        if (cd <= 0 && CurClip > 0 && !_fired)
        {
            _fired = true;
            CurClip -= 1;

            base.Fire(direction, strenght, aim);
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
