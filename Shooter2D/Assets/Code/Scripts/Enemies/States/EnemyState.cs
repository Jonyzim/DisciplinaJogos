using Pathfinding;
using UnityEngine;

namespace MWP.Enemies.States
{
    public abstract class EnemyState
    {
        protected float CurAstarTimer;
        protected Vector2 Direction;

        protected readonly Enemy Context;
        protected readonly EnemyStateFactory Factory;
        private int _i;
        private Path _path;

        protected EnemyState(Enemy context, EnemyStateFactory factory)
        {
            Context = context;
            Factory = factory;
        }

        public virtual void StartState()
        {
            OnFindPath += SetPath;
            _i = 1;
        }

        public virtual void UpdateState()
        {
            CurAstarTimer -= Time.deltaTime;
        }

        public virtual void ExitState()
        {
            OnFindPath -= SetPath;
        }

        protected void FollowPath()
        {
            // Moving towards camera
            Vector2 pos = Context.gameObject.transform.position;

            if (_path != null)
            {
                var distance = (Vector2)_path.vectorPath[_i] - pos;
                if (distance.magnitude <= Enemy.MinNodeDistance)
                {
                    if (_i < _path.vectorPath.Count - 1)
                        _i++;
                    distance = (Vector2)_path.vectorPath[_i] - pos;
                }

                Direction = Vector3.Normalize(distance);
            }
        }

        protected void CalculatePath(Vector2 destination)
        {
            Vector2 pos = Context.gameObject.transform.position;
            Context.Seeker.StartPath(pos, destination, OnFindPath);
        }

        private event OnPathDelegate OnFindPath;

        private void SetPath(Path p)
        {
            _path = p;
            _i = 1;
        }
    }
}