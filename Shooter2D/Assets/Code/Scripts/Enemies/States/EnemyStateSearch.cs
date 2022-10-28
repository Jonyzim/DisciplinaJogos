using UnityEngine;

namespace MWP.Enemies.States
{
    public class EnemyStateSearch : EnemyState
    {

        public EnemyStateSearch(Enemy context, EnemyStateFactory factory) : base(context, factory) { }


        public override void StartState()
        {
            base.StartState();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _curAstarTimer -= Time.deltaTime;

            FollowPath();
            if (_curAstarTimer <= 0)
            {
                CalculatePath(Camera.main.transform.position);
                SearchCharacters();
                _curAstarTimer = Enemy.ASTAR_TIMER;
            }

            Context.Move(_direction);

        }

        public override void ExitState()
        {
            base.ExitState();
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
}