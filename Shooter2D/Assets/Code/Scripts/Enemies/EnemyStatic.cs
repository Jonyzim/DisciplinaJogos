using UnityEngine;

namespace MWP.Enemies
{
    public class EnemyStatic : Enemy
    {
        [SerializeField] private float attackRange;

        [SerializeField] private LayerMask characterLayer;
        [SerializeField] private GameObject explosionFx;

        [SerializeField] private int damage;
        

        public override void Attack()
        {
            var pos = gameObject.transform.position;
            var charactersHit = Physics2D.OverlapCircleAll(pos, attackRange, characterLayer);
            Instantiate(explosionFx, pos, Quaternion.identity);
            
            foreach (var characterCollider in charactersHit)
            {
                var character = characterCollider.GetComponentInParent<Character>();
                if (character != null) character.UpdateHealth(-damage);
            }
        }
    }
}