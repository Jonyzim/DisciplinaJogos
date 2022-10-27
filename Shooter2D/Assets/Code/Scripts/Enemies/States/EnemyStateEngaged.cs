using UnityEngine;

namespace MWP.Enemies.States
{
    public class EnemyStateEngaged : EnemyState
    {
        private Pathfinding.Path path;
        private Vector2 _direction;
        private float _curTimer;

        public EnemyStateEngaged(Enemy context, EnemyStateFactory factory) : base(context, factory) { }


        public override void StartState()
        {
            OnFindPath += SetPath;
            _curTimer = _astarTimer;
        }

        public override void UpdateState()
        {
            _curTimer -= Time.deltaTime;
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
            OnFindPath -= SetPath;
        }

        private void FollowPath(Vector2 targetPos)
        {
            // Moving towards target
            Vector2 pos = Context.gameObject.transform.position;


            Context.Seeker.StartPath(pos, targetPos, OnFindPath);

            if (path != null)
            {
                Vector2 direction = Vector3.Normalize((Vector2)path.vectorPath[1] - pos);
                _direction = direction;
            }
        }

        private event Pathfinding.OnPathDelegate OnFindPath;

        private void SetPath(Pathfinding.Path p)
        {
            path = p;
        }

        private void Retreat(Vector2 targetPos)
        {
            Vector2 pos = Context.gameObject.transform.position;
            Vector2 direction = Vector3.Normalize(pos - targetPos);

            _direction = direction;
        }
    }
}