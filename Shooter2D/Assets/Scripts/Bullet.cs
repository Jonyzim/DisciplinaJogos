using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] protected float speed;

    protected Vector3 direction;

    public void SetDirection(Vector3 _direction){
        direction = _direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0)*speed;
    }
}
