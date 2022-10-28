using UnityEngine;

namespace MWP.Enemies.States
{
    public abstract class EnemyState
    {

        protected float _curAstarTimer;
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
            _curAstarTimer -= Time.deltaTime;
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

        protected void FollowPath()
        {
            // Moving towards camera
            Vector2 pos = Context.gameObject.transform.position;

            if (path != null)
            {
                Vector2 distance = (Vector2)path.vectorPath[i] - pos;
                if (distance.magnitude <= Enemy.MIN_NODE_DISTANCE)
                {
                    if (i < path.vectorPath.Count - 1)
                        i++;
                    distance = (Vector2)path.vectorPath[i] - pos;
                }

                _direction = Vector3.Normalize(distance);
            }
        }

        protected void CalculatePath(Vector2 destination)
        {
            Vector2 pos = Context.gameObject.transform.position;
            Context.Seeker.StartPath(pos, destination, OnFindPath);
        }

        private event Pathfinding.OnPathDelegate OnFindPath;

        protected void SetPath(Pathfinding.Path p)
        {
            path = p;
            i = 1;
        }
    }
}