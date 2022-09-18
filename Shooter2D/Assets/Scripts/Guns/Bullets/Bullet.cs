using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] protected float lifetime;
    protected Vector3 direction;
    [SerializeField] private GameObject destroyFxPrefab;
    
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
    
    public void DestroyBullet(float timer = 0)
    {
        Instantiate(destroyFxPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, timer);
    }
}
