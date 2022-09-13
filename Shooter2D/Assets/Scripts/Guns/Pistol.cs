using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    [SerializeField] protected GameObject bullet;


    protected override void Fire(Vector3 direction){
        GameObject _bullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
        _bullet.GetComponent<Bullet>().SetDirection(direction);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
