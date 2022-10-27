using UnityEngine;

namespace MWP.Enemies.States
{
    public class EnemyStateEngaged : EnemyState
    {
        private Pathfinding.Path path;

        public EnemyStateEngaged(Enemy context, EnemyStateFactory factory) : base(context, factory) { }


        public override void StartState()
        {
            OnFindPath += test;
        }

        public override void UpdateState()
        {
            Context.CurAttackTimer -= Time.fixedDeltaTime;
            if (Context.CurAttackTimer <= 0)
            {
                Context.Attack();
                Context.CurAttackTimer = Context.AttackDelay;
            }


            Vector2 targetPos = Context.Target.transform.position;
            float distanceToTarget = Vector2.Distance(targetPos, Context.gameObject.transform.position);
            if (distanceToTarget > Context.ResetDistance)
                Context.SwitchState(Factory.StateSearch);

            if (distanceToTarget > Context.MaxHoverDistance)
            {
                FollowPath(targetPos);
            }
            else if (distanceToTarget < Context.MinHoverDistance)
            {
                Retreat(targetPos);
            }
            else
            {
                float oldIntensity = Context.NoiseIntensity;
                Context.NoiseIntensity = 1;
                Context.Movement(Vector2.zero);
                Context.NoiseIntensity = oldIntensity;
            }

        }

        public override void ExitState()
        {
            OnFindPath -= test;
        }

        private void FollowPath(Vector2 targetPos)
        {
            // Moving towards target
            Vector2 pos = Context.gameObject.transform.position;


            Context.Seeker.StartPath(pos, targetPos, OnFindPath);

            if (path != null)
            {
                Vector2 direction = Vector3.Normalize((Vector2)path.vectorPath[1] - pos);
                Context.Movement(direction);
            }
        }

        private event Pathfinding.OnPathDelegate OnFindPath;

        private void test(Pathfinding.Path p)
        {
            path = p;
        }

        private void Retreat(Vector2 targetPos)
        {
            Vector2 pos = Context.gameObject.transform.position;
            Vector2 direction = Vector3.Normalize(pos - targetPos);

            Context.Movement(direction);
        }
    }
}