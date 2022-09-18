using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIFollow : Enemy
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject currentTarget;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    private bool founded = false;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(currentTarget != null){
            Movement();
        }
        else{
            CheckDistance();
        }
    }

    void CheckDistance(){
        float dist = Vector2.Distance(this.transform.position, target.transform.position);
        if(dist<=0) dist = -dist;
        if(dist <= range){
            currentTarget = target;
            founded = true;
        }
    }
    void FollowTarget(){
        Vector2 direction = target.transform.position - this.transform.position;
        float step = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, currentTarget.transform.position, step);
        if(direction.x <= 0) {
            this.transform.localScale = new Vector3(-1,1,1);
        }
        else {
            this.transform.localScale = new Vector3(1,1,1);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }
    protected override void Movement(){
        FollowTarget();
    }
    protected override void Damage()
    {
        //print("DAMAGE");
        StartCoroutine(DamageFx());
    }
}
