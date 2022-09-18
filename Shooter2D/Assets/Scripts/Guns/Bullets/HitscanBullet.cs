using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanBullet : Bullet
{
    [SerializeField] private float reach;

    public override void SetDirection(Vector3 _direction){
        base.SetDirection(_direction);

        // float distance;
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, reach);
        if(hitInfo.collider != null){
            // distance = Vector2.Distance(transform.position, hitInfo.point);
            transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y,0);
            Enemy enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
            if(enemy != null){
                enemy.Damage(transform.position);
                DestroyBullet();
            }
        }
        else{
            // distance = Vector2.Distance(transform.position, transform.position + direction * reach);
        }
    }
}
