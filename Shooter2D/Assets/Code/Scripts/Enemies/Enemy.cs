using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;
using TMPro;

// Follows camera and try to find players in radius A >
// Gets Player transform >
// Maintains distance X + random movement noise >
// If distance higher than Y go to Follows camera >
// If not Attack every Z seconds

public abstract class Enemy : MonoBehaviour
{
    public float CurAttackTimer
    {
        get => _curAttackTimer;
        set => _curAttackTimer = value;
    }

    public float DetectionRadius;
    public float HoverDistance;
    public float ResetDistance;
    public float AttackDelay;
    public float Speed;
    public LayerMask CharacterLayer;

    public GameObject Target;


    private EnemyState _curEnemyState;
    private EnemyStateFactory _factory;


    public int Life => (int)_life;
    public bool IsDead => _isDead;

    [SerializeField] protected GameObject HitFxPrefab;
    [SerializeField] protected GameObject DeathFxPrefab;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _fxSpeed = 0.05f;
    [SerializeField] private GameObject _scoreViewPrefab;
    [SerializeField] private Rigidbody2D _rigidBody;

    private float _curAttackTimer;
    private Color _startColor;
    private Color _damageColor = new Color(1f, 0, 0, 1f);
    private Transform _scoreCanvas;
    private float _life = 100f;
    private bool _isDead = false;

    //Methods
    public abstract void Attack();

    public void SwitchState(EnemyState newState)
    {
        _curEnemyState?.ExitState();
        newState.StartState();
        _curEnemyState = newState;
    }

    public virtual bool TakeDamage(Vector3? pos, float damage)
    {
        if (_isDead) return false;

        Vector3 position = pos ?? transform.position;

        Instantiate(HitFxPrefab, position, Quaternion.identity);
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

    public virtual void Movement(Vector2 direction)
    {
        Vector2 movement = direction * Speed;
        _rigidBody.velocity = movement;
    }

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


    protected virtual void Awake()
    {
        _factory = new EnemyStateFactory(this);
    }

    //Unity Functions
    protected virtual void Start()
    {
        SwitchState(_factory.StateSearch);

        _startColor = _spriteRenderer.color;
        _scoreCanvas = GameObject.FindGameObjectWithTag("ScoreCanvas").transform;
    }

    protected virtual void FixedUpdate()
    {
        _curEnemyState.UpdateState();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, HoverDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ResetDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
    }
}
