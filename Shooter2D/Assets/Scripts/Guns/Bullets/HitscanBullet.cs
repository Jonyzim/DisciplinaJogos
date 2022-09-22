using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanBullet : Bullet
{
    [SerializeField] private float reach;

    [SerializeField] private GameObject particles;
    private Color lineColor;
    private float cur_lifetime;
    [SerializeField] LayerMask ignoreLayer;


    public override void SetVariables(Vector2 _direction, int strenght){
        base.SetVariables(_direction, strenght);
        Vector2 position;

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, reach, ~ignoreLayer);

        if(hitInfo.collider != null){
            position = hitInfo.point;

            Enemy enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
            if(enemy != null && !enemy.IsDead)
            {
                DamageOnEnemy(enemy,position);
            }
        }
        else{
            position = transform.position + direction * reach;
        }


        particles.transform.localScale = new Vector3(Vector3.Distance(transform.position, position), 1, 1);
        particles.transform.SetParent(null, true);
        particles.gameObject.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction));

        Destroy(particles.gameObject, 2);
        transform.position = new Vector3(position.x, position.y,0);
    }

}
