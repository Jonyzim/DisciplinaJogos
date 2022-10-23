using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateEngaged : EnemyState
{
    public EnemyStateEngaged(Enemy context, EnemyStateFactory factory) : base(context, factory) { }

    public override void StartState()
    {
        Debug.Log("Engaging");
    }

    public override void UpdateState()
    {

        Context.CurAttackTimer -= Time.fixedDeltaTime;
        if (Context.CurAttackTimer <= 0)
        {
            Context.Attack();
            Context.CurAttackTimer = Context.AttackDelay;
        }

        Vector2 direction = Vector3.Normalize(Context.Target.transform.position - Context.gameObject.transform.position);
        float distanceToTarget = Vector2.Distance(Context.Target.transform.position, Context.gameObject.transform.position);

        // TODO: Implement Noise Function
        Vector2 noise = Vector2.zero;


        direction = distanceToTarget > Context.HoverDistance ? direction + noise : -direction + noise;

        Context.Movement(direction);




        if (distanceToTarget > Context.ResetDistance)
        {
            Context.SwitchState(Factory.StateSearch);
        }
    }

    public override void ExitState()
    {

    }
}
