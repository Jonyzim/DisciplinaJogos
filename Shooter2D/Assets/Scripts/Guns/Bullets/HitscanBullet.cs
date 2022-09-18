using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanBullet : Bullet
{
    [SerializeField] private float reach;

    [SerializeField] private LineRenderer bulletTrail;

    public override void SetDirection(Vector3 _direction){
        base.SetDirection(_direction);
        Vector3 position;
        bulletTrail.transform.position = Vector3.zero;
        bulletTrail.positionCount = 2;
        bulletTrail.SetPosition(0, transform.position);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, reach);
        if(hitInfo.collider != null){
            position = new Vector3(hitInfo.point.x, hitInfo.point.y,0);
            Enemy enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
            if(enemy != null){
                enemy.Damage(position);
            }
            bulletTrail.SetPosition(1, position);
        }
        else{
            position = transform.position + direction * reach;
            bulletTrail.SetPosition(1, position);
        }
    }
}
