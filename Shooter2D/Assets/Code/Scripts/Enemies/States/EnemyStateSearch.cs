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

        FollowPath();
        SearchCharacters();

    }

    public override void ExitState()
    {

    }

    private void FollowPath()
    {
        // Moving towards camera
        Vector2 pos = Context.gameObject.transform.position;
        Vector2 camPos = Camera.main.transform.position;

        Pathfinding.Path path = Context.Seeker.StartPath(pos, camPos);
        path.BlockUntilCalculated();
        Vector2 direction = Vector3.Normalize((Vector2)path.vectorPath[1] - pos);

        Context.Movement(direction);
    }

    private void SearchCharacters()
    {
        Vector2 pos = Context.gameObject.transform.position;

        // Checking for targets
        Collider2D[] charactersHit = Physics2D.OverlapCircleAll(pos, Context.DetectionRadius, Context.CharacterLayer);

        if (charactersHit.Length > 0)
        {
            float minDistance = float.PositiveInfinity;
            foreach (Collider2D characterCollider in charactersHit)
            {
                float distanceToCharacter = Vector2.Distance(pos, characterCollider.transform.position);

                if (distanceToCharacter < minDistance)
                {
                    Context.Target = characterCollider.gameObject;
                    minDistance = distanceToCharacter;
                }
            }
            Context.SwitchState(Factory.StateEngaged);
        }
    }

}
