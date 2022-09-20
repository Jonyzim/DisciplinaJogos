using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;
using TMPro;
public abstract class Enemy : MonoBehaviour
{
    Color startColor;
    Color damageColor = new Color(1f, 0, 0, 1f);
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] float fxSpeed = 0.05f;
    [SerializeField] protected GameObject hitFxPrefab;
    [SerializeField] protected GameObject deathFxPrefab;
    private Transform scoreCanvas;
    [SerializeField] private GameObject scoreViewPrefab;
    private float life = 100f;
    public int Life => (int)life;
    bool isDead=false;
    public bool IsDead => isDead;
    protected abstract void Movement();
    protected virtual void Death()
    {
        Vector3 pos = transform.position;
        pos.y += 1f;
        Instantiate(deathFxPrefab, pos, Quaternion.identity);
        Destroy(gameObject,0.1f); 
        
        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }

    
    public virtual bool Damage(Vector3 pos, float damage){
        if (isDead) return false;

        Instantiate(hitFxPrefab, pos, Quaternion.identity);
        StartCoroutine(DamageFx());
        float damageCaused = Mathf.Min(life, damage);
        life -= damage;
        Vector3 scorePos = transform.position;
        scorePos.y += 1f;
        GameObject score = Instantiate(scoreViewPrefab,scorePos,Quaternion.identity, scoreCanvas);
        score.GetComponentInChildren<TMP_Text>().text = damageCaused.ToString();
        if (life <= 0)
        {
            Death();
            isDead = true;
            return true;
        }
        return false;
    }


    protected virtual void Start()
    {
        startColor = spriteRenderer.color;
        scoreCanvas = GameObject.FindGameObjectWithTag("ScoreCanvas").transform;
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
        spriteRenderer.color = Color.Lerp(initial, final, 1f);
    }
    public IEnumerator DamageFx()
    {

        yield return StartCoroutine(ChangeColorFx(startColor, damageColor));
        yield return StartCoroutine(ChangeColorFx(damageColor, startColor));
    }

    // protected virtual void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.CompareTag(BULLET_TAG))
    //     {
    //         Damage();
    //         col.gameObject.GetComponent<Bullet>().DestroyBullet(); 
    //         Instantiate(hitFxPrefab, col.gameObject.transform.position, Quaternion.identity);
    //     }
    // }


}
