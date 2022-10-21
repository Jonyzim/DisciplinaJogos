using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauseDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool canCauseDamage = true;
    IEnumerator DamageDelay()
    {
        canCauseDamage = false;
        yield return new WaitForSeconds(0.2f);
        canCauseDamage = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canCauseDamage)
        {
            Character character = collision.gameObject.GetComponent<Character>();
            if (character != null)
            {
                character.ChangeHealth(damage);
                StartCoroutine(DamageDelay());
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canCauseDamage)
        {

            Character character = collision.gameObject.GetComponent<Character>();
            if (character != null)
            {
                character.ChangeHealth(damage);
                StartCoroutine(DamageDelay());
            }
        }
    }
}