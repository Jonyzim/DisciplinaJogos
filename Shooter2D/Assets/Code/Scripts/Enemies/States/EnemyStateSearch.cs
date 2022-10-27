using UnityEngine;

namespace MWP.Enemies.States
{
    public class EnemyStateSearch : EnemyState
    {
        private Pathfinding.Path path;
        private Vector2 _direction;
        private const float timer = 0.13f;
        private float _curTimer;

        public EnemyStateSearch(Enemy context, EnemyStateFactory factory) : base(context, factory) { }


        public override void StartState()
        {
            OnFindPath += SetPath;
            _curTimer = timer;
        }

        public override void UpdateState()
        {
            _curTimer -= Time.deltaTime;
            Context.Movement(_direction);
            if (_curTimer <= 0)
            {
                FollowPath();
                SearchCharacters();
                _curTimer = timer;
            }

        }

        public override void ExitState()
        {
            OnFindPath -= SetPath;
        }

        private void FollowPath()
        {
            // Moving towards camera
            Vector2 pos = Context.gameObject.transform.position;
            Vector2 camPos = Camera.main.transform.position;

            Context.Seeker.StartPath(pos, camPos, OnFindPath);

            if (path != null)
            {
                _direction = Vector3.Normalize((Vector2)path.vectorPath[1] - pos);
            }
        }

        private event Pathfinding.OnPathDelegate OnFindPath;

        private void SetPath(Pathfinding.Path p)
        {
            path = p;
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