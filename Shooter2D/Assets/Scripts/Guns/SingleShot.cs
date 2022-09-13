using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SingleShot : Gun
{
    [Header("SingleShotSpecifics")]
    [SerializeField] protected GameObject bullet;

    protected abstract void FireProps();
    protected override void Fire(Vector3 direction){
        GameObject _bullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
        _bullet.GetComponent<Bullet>().SetDirection(direction);

    
        FireProps();
        cd = 1/rof;
        cur_magazine -= 1;
    }

    protected abstract override void ReloadProps(float time);

    // Start is called before the first frame update
    protected virtual new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected virtual new void Update()
    {
        base.Update();

        if (Input.GetButtonDown("Fire1") && cur_magazine > 0)
        {
            
            if(cd <= 0){
                Fire(direction);
            }
        }
    }
}
