using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] protected float lifetime;
    protected Vector3 direction;
    [SerializeField] private GameObject destroyFxPrefab;

    [SerializeField] private Rigidbody2D rgbd;
    public void SetDirection(Vector3 _direction){
        direction = _direction;
        rgbd.AddForce(direction * speed, ForceMode2D.Impulse);
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

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            DestroyBullet();
        }
    }
}
