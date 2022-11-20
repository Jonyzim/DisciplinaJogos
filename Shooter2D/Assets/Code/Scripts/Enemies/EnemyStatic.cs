using System.Collections;
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
            StartCoroutine(nameof(Explode));
        }

        private IEnumerator Explode()
        {
            var oldSpeed = Speed;
            Speed = 0;
            yield return new WaitForSeconds(1);
            var pos = gameObject.transform.position;
            var charactersHit = Physics2D.OverlapCircleAll(pos, attackRange, characterLayer);
            Instantiate(explosionFx, pos, Quaternion.identity);
            
            foreach (var characterCollider in charactersHit)
            {
                var character = characterCollider.GetComponentInParent<Character>();
                if (character != null) character.UpdateHealth(-damage);
            }

            Speed = oldSpeed;
        }
    }
}