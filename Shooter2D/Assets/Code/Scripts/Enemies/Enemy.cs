using System;
using System.Collections;
using MWP.Enemies.States;
using MWP.GameStates;
using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace MWP.Enemies
{
    // Follows camera and try to find players in radius A >
    // Gets Player transform >
    // Maintains distance X + random movement noise >
    // If distance higher than Y go to Follows camera >
    // If not Attack every Z seconds

    [DisallowMultipleComponent]
    public abstract class Enemy : MonoBehaviour
    {
        public const float AstarTimer = 0.53f;
        public const float MinNodeDistance = 0.2f;

        [FormerlySerializedAs("NoiseIntensity")] public float noiseIntensity;

        [FormerlySerializedAs("IsHovering")] [HideInInspector] public bool isHovering;

        [FormerlySerializedAs("NoiseSmoothness")] public float noiseSmoothness;

        public Seeker Seeker;
        public float DetectionRadius;
        public float MinHoverDistance;
        public float MaxHoverDistance;
        public float ResetDistance;
        public float Speed;
        public float AttackDelay;
        public LayerMask CharacterLayer;

        public GameObject Target;

        [SerializeField] protected GameObject HitFxPrefab;
        [SerializeField] protected GameObject DeathFxPrefab;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _fxSpeed = 0.05f;
        [SerializeField] private GameObject _scoreViewPrefab;
        [SerializeField] private Rigidbody2D _rigidBody;


        private EnemyState _curEnemyState;
        [SerializeField] private Color _damageColor = Color.red;
        private EnemyStateFactory _factory;
        
        [SerializeField] private float _life = 100f;
        private float _movementSeedX;
        private float _movementSeedY;
        private Transform _scoreCanvas;
        private Color _startColor;

        public float CurAttackTimer { get; set; }


        public int Life => (int)_life;
        public bool IsDead { get; private set; }

        protected virtual void Awake()
        {
            _factory = new EnemyStateFactory(this);
            _movementSeedX = Random.Range(0f, 10f);
            _movementSeedY = Random.Range(0f, 10f);
        }

        //Unity Functions
        protected virtual void Start()
        {
            SwitchState(_factory.StateSearch);
            _life *= GameManager.Instance.HpMultiplier;
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
            var position = transform.position;
            Gizmos.DrawWireSphere(position, MinHoverDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(position, MaxHoverDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, ResetDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(position, DetectionRadius);
        }


        public event Action OnDeath;

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
            if (IsDead) return false;

            var position = pos ?? transform.position;

            Instantiate(HitFxPrefab, position, Quaternion.identity);
            StartCoroutine(DamageFx());
            var damageCaused = Mathf.Min(_life, damage);
            _life -= damage;
            var scorePos = transform.position;
            scorePos.y += 1f;
            var score = Instantiate(_scoreViewPrefab, scorePos, Quaternion.identity, _scoreCanvas);

            score.GetComponentInChildren<TMP_Text>().text = damageCaused.ToString();

            if (_life <= 0)
            {
                Death();
                IsDead = true;
                return true;
            }

            return false;
        }

        private IEnumerator DamageFx()
        {
            yield return StartCoroutine(ChangeColorFx(_startColor, _damageColor));
            yield return StartCoroutine(ChangeColorFx(_damageColor, _startColor));
        }

        public virtual void Move(Vector2 direction, bool randomMovement = false)
        {
            var intensity = randomMovement ? 1f : noiseIntensity;
            direction += PerlinNoiseDirection() * intensity;
            var movement = direction * Speed;
            _rigidBody.velocity = Vector2.Lerp(_rigidBody.velocity, movement, 0.4f);
        }

        protected virtual void Death()
        {
            var pos = transform.position;
            pos.y += 1f;
            Instantiate(DeathFxPrefab, pos, Quaternion.identity);
            OnDeath?.Invoke();
            Destroy(gameObject, 0.1f);

            var scripts = gameObject.GetComponents<MonoBehaviour>();
            foreach (var script in scripts) script.enabled = false;
        }

        private Vector2 PerlinNoiseDirection()
        {
            var vector = new Vector2();
            var time = Time.fixedTime;
            vector.x = (Mathf.PerlinNoise(time / noiseSmoothness, _movementSeedX) - 0.5f) * 2f;
            vector.y = (Mathf.PerlinNoise(time / noiseSmoothness, _movementSeedY) - 0.5f) * 2f;

            return Vector3.Normalize(vector);
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
    }
}