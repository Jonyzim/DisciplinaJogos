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
    [SerializeField] protected Player playerThatShooted;

    public void SetPlayer(Player p)
    {
        playerThatShooted = p;
    }

    public virtual void SetDirection(Vector3 _direction){
        direction = _direction;
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
       
        if(playerThatShooted!=null)
            playerThatShooted.AddScore(n);
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
