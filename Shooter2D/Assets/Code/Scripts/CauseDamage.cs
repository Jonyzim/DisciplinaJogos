using System.Collections;
using UnityEngine;

namespace MWP
{
    public class CauseDamage : MonoBehaviour
    {
        [SerializeField] private int damage;
        private bool canCauseDamage = true;

        private IEnumerator DamageDelay()
        {
            canCauseDamage = false;
            yield return new WaitForSeconds(0.2f);
            canCauseDamage = true;
        }
        // private void OnTriggerEnter2D(Collider2D collision)
        // {
        //     if (canCauseDamage)
        //     {
        //         Character character = collision.gameObject.GetComponent<Character>();
        //         if (character != null)
        //         {
        //             character.UpdateHealth(-damage);
        //             StartCoroutine(DamageDelay());
        //         }
        //     }
        // }
        // private void OnTriggerStay2D(Collider2D collision)
        // {
        //     if (canCauseDamage)
        //     {

        //         Character character = collision.gameObject.GetComponent<Character>();
        //         if (character != null)
        //         {
        //             character.UpdateHealth(-damage);
        //             StartCoroutine(DamageDelay());
        //         }
        //     }
        // }
    }
}