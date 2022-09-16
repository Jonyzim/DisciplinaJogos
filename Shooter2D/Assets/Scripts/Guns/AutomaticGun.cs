using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutomaticGun : Gun
{
    [Header("AutomaticSpecifics")]
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float spread;
    [SerializeField] protected Transform spawnTransf;
    protected override void Fire(Vector3 direction){
        base.Fire(direction);
        GameObject _bullet = Instantiate(bullet, spawnTransf.position, Quaternion.Euler(0, 0, 0));
        _bullet.GetComponent<Bullet>().SetDirection(direction);
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

        if (Input.GetButton("Fire1") && cur_magazine > 0)
        {
            
            if(cd <= 0){
                Vector3 _direction = Quaternion.AngleAxis(-Random.Range(-spread, spread), new Vector3(0, 0, 1)) * direction;
                Fire(_direction);
            }
        }
    }
}
