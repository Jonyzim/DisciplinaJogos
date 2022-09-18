using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanBullet : Bullet
{
    [SerializeField] private float reach;

    [SerializeField] private LineRenderer bulletTrail;
    private Color lineColor;
    private float cur_lifetime;


    protected override void Start(){
        cur_lifetime = lifetime;
    }
    void Update(){
        cur_lifetime -= Time.deltaTime;
        lineColor = new Color(1f, 1f, 1f, cur_lifetime/lifetime);
        bulletTrail.startColor = lineColor;
        bulletTrail.endColor = lineColor;
    }

    public override void SetDirection(Vector3 _direction){
        base.SetDirection(_direction);
        bulletTrail.positionCount = 2;
        bulletTrail.SetPosition(0, transform.position);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, reach);
        if(hitInfo.collider != null){
            transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y,0);

            Enemy enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
            if(enemy != null){
                enemy.Damage(transform.position);
            }
        }
        else{
            transform.position = transform.position + direction * reach;
        }
        bulletTrail.SetPosition(1, transform.position);
        bulletTrail.transform.position = Vector3.zero;
        DestroyBullet(lifetime);
    }

}
