using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutomaticGun : Gun
{
    //[Header("AutomaticSpecifics")]

    protected override void Fire(Vector3 direction, int strenght){
        if(cd <= 0 && cur_magazine > 0){
            base.Fire(direction, strenght);
        }
    }

    protected abstract override void ReloadProps(float time);

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
