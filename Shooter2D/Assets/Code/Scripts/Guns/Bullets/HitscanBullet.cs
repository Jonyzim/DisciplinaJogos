using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanBullet : Bullet
{

    [SerializeField] private float _reach;
    [SerializeField] private LayerMask IgnoreLayer;
    [SerializeField] private GameObject _particles;
    private Color _lineColor;
    private float _curLifetime;


    public override void SetVariables(Vector2 direction, int strenght, int damage)
    {
        base.SetVariables(direction, strenght, damage);
        Vector2 position;

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Direction, _reach, ~IgnoreLayer);

        if (hitInfo.collider != null)
        {
            position = hitInfo.point;

            Enemy enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
            if (enemy != null && !enemy.IsDead)
            {
                DamageOnEnemy(enemy, position);
            }
        }
        else
        {
            position = transform.position + Direction * _reach;
        }


        _particles.transform.localScale = new Vector3(Vector3.Distance(transform.position, position), 1, 1);
        _particles.transform.SetParent(null, true);
        _particles.gameObject.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, Direction));

        Destroy(_particles.gameObject, 2);
        transform.position = new Vector3(position.x, position.y, 0);
    }

}
