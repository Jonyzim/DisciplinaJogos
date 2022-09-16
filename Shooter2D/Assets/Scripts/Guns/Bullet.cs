using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] protected float lifetime;
    protected Vector3 direction;
    [SerializeField] private GameObject destroyFxPrefab;
    public void SetDirection(Vector3 _direction){
        direction = _direction;
    }
    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(lifetime);
        DestroyBullet();
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DestroyDelay());
        Destroy(gameObject, lifetime);
    }
    
    public void DestroyBullet()
    {
        Instantiate(destroyFxPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0)*speed;
    }
}
