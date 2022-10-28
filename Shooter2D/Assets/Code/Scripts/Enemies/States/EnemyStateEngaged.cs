using UnityEngine;

namespace MWP.Enemies.States
{
    public class EnemyStateEngaged : EnemyState
    {
        public EnemyStateEngaged(Enemy context, EnemyStateFactory factory) : base(context, factory) { }


        public override void StartState()
        {
            base.StartState();
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
            float distanceToTarget = Vector2.Distance(targetPos, Context.gameObject.transform.position);

            if (distanceToTarget > Context.ResetDistance)
            {
                Context.IsHovering = false;
                Context.SwitchState(Factory.StateSearch);

            }

            if (distanceToTarget > Context.MaxHoverDistance)
            {
                if (_curTimer <= 0)
                {
                    FollowPath(targetPos);
                    Context.IsHovering = false;
                    _curTimer = _astarTimer;
                }
            }
            else if (distanceToTarget < Context.MinHoverDistance)
            {
                Retreat(targetPos);
                Context.IsHovering = false;
            }
            else
            {
                float oldIntensity = Context.NoiseIntensity;
                _direction = Vector2.zero;
                Context.IsHovering = true;
            }

            Context.Move(_direction, Context.IsHovering);

        }

        public override void ExitState()
        {
            base.ExitState();
        }

        private void Retreat(Vector2 targetPos)
        {
            Vector2 pos = Context.gameObject.transform.position;
            Vector2 direction = Vector3.Normalize(pos - targetPos);

            _direction = direction;
        }
    }
}