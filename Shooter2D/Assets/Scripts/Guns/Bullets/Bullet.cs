using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] protected float lifetime;
    protected Vector3 direction;
    [SerializeField] private GameObject destroyFxPrefab;
    [SerializeField] protected float damageCaused;
    [SerializeField] protected int ownerId;

    public void SetPlayer(int id)
    {
        ownerId = id;
    }

    public virtual void SetVariables(Vector2 _direction, int strenght){
        direction = _direction;
        damageCaused *= ((float)strenght/100);
    }

    // IEnumerator DestroyDelay()
    // {
    //     yield return new WaitForSeconds(lifetime);
    //     DestroyBullet();
    // }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //StartCoroutine(DestroyDelay());
        DestroyBullet(lifetime);
    }

    protected virtual void AddPlayerScore(int n)
    {
        if(ownerId != -1)
            GameEvents.current.ScoreUpdate(ownerId, n);
    }
    protected virtual void DamageOnEnemy(Enemy enemy,Vector3 pos)
    {

        //if(enemy.Damage(transform.position, damageCaused))
        //    AddPlayerScore(10);
        int life = enemy.Life;
        enemy.Damage(pos, damageCaused);
        AddPlayerScore(Mathf.Min(life, (int)damageCaused));
    }
    public void DestroyBullet(float timer = 0)
    {
        Instantiate(destroyFxPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, timer);
    }
}
