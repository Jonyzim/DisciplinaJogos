using UnityEngine;

namespace MWP.Enemies.States
{
    public class EnemyStateSearch : EnemyState
    {
        public EnemyStateSearch(Enemy context, EnemyStateFactory factory) : base(context, factory)
        {
        }


        public override void StartState()
        {
            base.StartState();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            CurAstarTimer -= Time.deltaTime;

            FollowPath();
            if (CurAstarTimer <= 0)
            {
                CalculatePath(Camera.main.transform.position);
                SearchCharacters();
                CurAstarTimer = Enemy.AstarTimer;
            }

            Context.Move(Direction);
        }


        private void SearchCharacters()
        {
            Vector2 pos = Context.gameObject.transform.position;

            // Checking for targets
            var charactersHit = Physics2D.OverlapCircleAll(pos, Context.DetectionRadius, Context.CharacterLayer);

            if (charactersHit.Length <= 0) return;
            var minDistance = float.PositiveInfinity;
            foreach (var characterCollider in charactersHit)
            {
                var distanceToCharacter = Vector2.Distance(pos, characterCollider.transform.position);

                if (!(distanceToCharacter < minDistance)) continue;
                Context.Target = characterCollider.gameObject;
                minDistance = distanceToCharacter;
            }

            Context.SwitchState(Factory.StateEngaged);
        }
    }
}