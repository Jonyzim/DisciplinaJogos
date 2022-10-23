using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSearch : EnemyState
{
    public EnemyStateSearch(Enemy context, EnemyStateFactory factory) : base(context, factory) { }

    public override void StartState()
    {
        Debug.Log("Moving towards camera");
    }

    public override void UpdateState()
    {
        Vector2 pos = Context.gameObject.transform.position;
        float minDistance = float.PositiveInfinity;

        // Moving towards camera
        Vector2 camPos = Camera.main.transform.position;
        Vector2 direction = Vector3.Normalize(camPos - pos);
        Context.Movement(direction);

        // Checking for targets
        Collider2D[] charactersHit = Physics2D.OverlapCircleAll(pos, Context.DetectionRadius, Context.CharacterLayer);
        foreach (Collider2D characterCollider in charactersHit)
        {
            float distanceToCharacter = Vector2.Distance(pos, characterCollider.transform.position);

            if (distanceToCharacter < minDistance)
            {
                Context.Target = characterCollider.gameObject;
                minDistance = distanceToCharacter;
            }
        }

        if (charactersHit.Length > 0)
        {
            Context.SwitchState(Factory.StateEngaged);
        }

    }

    public override void ExitState()
    {

    }

}
