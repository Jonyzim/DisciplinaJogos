using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public abstract class Enemy : MonoBehaviour
{
    Color startColor;
    Color damageColor = new Color(1f, 0, 0, 1f);
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] float fxSpeed = 0.05f;

    protected abstract void Movement();
    protected abstract void Damage();


    protected virtual void Start()
    {
        startColor = spriteRenderer.color;
    }
    IEnumerator ChangeColorFx(Color initial, Color final)
    {
        float t = 0;
        while (t <= 1f)
        {
            spriteRenderer.color = Color.Lerp(initial, final, t);
            t += fxSpeed;

            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator DamageFx()
    {

        yield return StartCoroutine(ChangeColorFx(startColor, damageColor));
        yield return StartCoroutine(ChangeColorFx(damageColor, startColor));
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(BULLET_TAG))
        {
            Damage();
            col.GetComponent<Bullet>().DestroyBullet();
        }
    }


}
