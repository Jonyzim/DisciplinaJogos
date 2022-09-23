using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;
using TMPro;

public abstract class Enemy : MonoBehaviour
{
    public int Life => (int)_life;
    public bool IsDead => _isDead;
    [SerializeField] protected GameObject HitFxPrefab;
    [SerializeField] protected GameObject DeathFxPrefab;
    private Color _startColor;
    private Color _damageColor = new Color(1f, 0, 0, 1f);
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _fxSpeed = 0.05f;
    [SerializeField] private GameObject _scoreViewPrefab;
    private Transform _scoreCanvas;
    private float _life = 100f;
    private bool _isDead = false;

    //Methods
    public virtual bool Damage(Vector3 pos, float damage)
    {
        if (_isDead) return false;

        Instantiate(HitFxPrefab, pos, Quaternion.identity);
        StartCoroutine(DamageFx());
        float damageCaused = Mathf.Min(_life, damage);
        _life -= damage;
        Vector3 scorePos = transform.position;
        scorePos.y += 1f;
        GameObject score = Instantiate(_scoreViewPrefab, scorePos, Quaternion.identity, _scoreCanvas);
        score.GetComponentInChildren<TMP_Text>().text = damageCaused.ToString();
        if (_life <= 0)
        {
            Death();
            _isDead = true;
            return true;
        }
        return false;
    }

    public IEnumerator DamageFx()
    {
        yield return StartCoroutine(ChangeColorFx(_startColor, _damageColor));
        yield return StartCoroutine(ChangeColorFx(_damageColor, _startColor));
    }

    protected abstract void Movement();

    protected virtual void Death()
    {
        Vector3 pos = transform.position;
        pos.y += 1f;
        Instantiate(DeathFxPrefab, pos, Quaternion.identity);
        Destroy(gameObject, 0.1f);

        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }

    private IEnumerator ChangeColorFx(Color initial, Color final)
    {
        float t = 0;
        while (t <= 1f)
        {
            _spriteRenderer.color = Color.Lerp(initial, final, t);
            t += _fxSpeed;

            yield return new WaitForEndOfFrame();
        }
        _spriteRenderer.color = Color.Lerp(initial, final, 1f);
    }

    //Unity Functions
    protected virtual void Start()
    {
        _startColor = _spriteRenderer.color;
        _scoreCanvas = GameObject.FindGameObjectWithTag("ScoreCanvas").transform;
    }

}
