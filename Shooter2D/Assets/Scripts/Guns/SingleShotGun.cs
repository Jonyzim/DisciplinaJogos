using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleShotGun : Gun
{
    //[Header("SingleShotSpecifics")]
    private bool _fired = false;

    public override void Fire(Vector2 direction, int strenght, int aim){
        if(cd <= 0 && cur_magazine > 0 && !_fired){
            _fired = true;
            base.Fire(direction, strenght, aim);
        }
        
    }
    public override void ReleaseFire()
    {
        _fired = false;
    }

    protected abstract override void ReloadProps(float time);

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
