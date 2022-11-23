using UnityEngine;

namespace MWP.Enemies.States
{
    public class EnemyStateEngaged : EnemyState
    {
        public EnemyStateEngaged(Enemy context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void StartState()
        {
            base.StartState();
            Context.CurAttackTimer = Context.AttackDelay;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            Context.CurAttackTimer -= Time.fixedDeltaTime;

            if (Context.CurAttackTimer <= 0)
            {
                Context.Attack();
                Context.CurAttackTimer = Context.AttackDelay;
            }


            Vector2 targetPos = Context.Target.transform.position;
            var distanceToTarget = Vector2.Distance(targetPos, Context.gameObject.transform.position);

            if (distanceToTarget > Context.ResetDistance || !Context.Target.activeSelf)
            {
                Context.isHovering = false;
                Context.SwitchState(Factory.StateSearch);
            }

            else if (distanceToTarget > Context.MaxHoverDistance)
            {
                FollowPath();
                if (CurAstarTimer <= 0)
                {
                    CalculatePath(targetPos);
                    Context.isHovering = false;
                    CurAstarTimer = Enemy.AstarTimer;
                }
            }
            else if (distanceToTarget < Context.MinHoverDistance)
            {
                Retreat(targetPos);
                Context.isHovering = false;
            }
            else
            {
                var oldIntensity = Context.noiseIntensity;
                Direction = Vector2.zero;
                Context.isHovering = true;
            }

            Context.Move(Direction, Context.isHovering);
        }

        private void Retreat(Vector2 targetPos)
        {
            Vector2 pos = Context.gameObject.transform.position;
            Vector2 direction = Vector3.Normalize(pos - targetPos);

            Direction = direction;
        }
    }
}