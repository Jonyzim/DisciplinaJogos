using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] protected float Speed;
    [SerializeField] protected float Lifetime;
    [SerializeField] protected float DamageCaused;
    [SerializeField] protected int OwnerId;
    protected Vector3 Direction;
    [SerializeField] private GameObject _destroyFxPrefab;

    //Methods
    public void SetPlayer(int id)
    {
        OwnerId = id;
    }

    public virtual void SetVariables(Vector2 direction, int strenght)
    {
        Direction = direction;
        DamageCaused *= ((float)strenght / 100);
    }

    // IEnumerator DestroyDelay()
    // {
    //     yield return new WaitForSeconds(lifetime);
    //     DestroyBullet();
    // }

    public void DestroyBullet(float timer = 0)
    {
        Instantiate(_destroyFxPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, timer);
    }

    protected virtual void AddPlayerScore(int n)
    {
        if (OwnerId != -1)
            GameEvents.s_instance.ScoreUpdate(OwnerId, n);
    }
    protected virtual void DamageOnEnemy(Enemy enemy, Vector3 pos)
    {

        //if(enemy.Damage(transform.position, damageCaused))
        //    AddPlayerScore(10);
        int life = enemy.Life;
        enemy.Damage(pos, DamageCaused);
        AddPlayerScore(Mathf.Min(life, (int)DamageCaused));
    }

    //Unity Methods
    protected virtual void Start()
    {
        //StartCoroutine(DestroyDelay());
        DestroyBullet(Lifetime);
    }
}
