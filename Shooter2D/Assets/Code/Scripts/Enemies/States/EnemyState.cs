using UnityEngine;

namespace MWP.Enemies.States
{
    public abstract class EnemyState
    {
        protected const float _astarTimer = 0.53f;
        protected const float _minNodeDistance = 0.1f;
        protected float _curTimer;
        protected Pathfinding.Path path;
        protected Vector2 _direction;
        protected int i;

        protected Enemy Context;
        protected EnemyStateFactory Factory;

        public virtual void StartState()
        {
            OnFindPath += SetPath;
            i = 1;
        }
        public virtual void UpdateState()
        {
            _curTimer -= Time.deltaTime;
        }
        public virtual void ExitState()
        {
            OnFindPath -= SetPath;
        }

        public EnemyState(Enemy context, EnemyStateFactory factory)
        {
            Context = context;
            Factory = factory;
        }

        protected void FollowPath(Vector2 destination)
        {
            // Moving towards camera
            Vector2 pos = Context.gameObject.transform.position;
            Vector2 camPos = destination;

            if (_curTimer <= 0)
            {
                Context.Seeker.StartPath(pos, camPos, OnFindPath);
                _curTimer = _astarTimer;
            }

            if (path != null)
            {
                Vector2 distance = (Vector2)path.vectorPath[i] - pos;
                if (distance.magnitude <= _minNodeDistance)
                {
                    i++;
                    distance = (Vector2)path.vectorPath[i] - pos;
                }

                _direction = Vector3.Normalize(distance);
            }
        }

        private event Pathfinding.OnPathDelegate OnFindPath;

        protected void SetPath(Pathfinding.Path p)
        {
            path = p;
            i = 1;
        }
    }
}