using System;
using System.Collections;
using System.Collections.Generic;
using MWP.Buffs;
using MWP.GameStates;
using MWP.Guns;
using MWP.Interactables;
using MWP.Misc;
using MWP.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Events;

namespace MWP
{
    [DisallowMultipleComponent]
    public class Character : MonoBehaviour
    {
        private const float BaseSpeed = 10;
        [SerializeField] private float dashCooldown;
        [SerializeField] private TrailRenderer trail;

        public GameObject weaponSlot;
        
        [SerializeField] private float dashPower;

        [SerializeField] private float dashTime = 0.1f;


        [FormerlySerializedAs("EquippedGun")] public Gun equippedGun;
        [SerializeField] private GameObject _defaultGun;

        [FormerlySerializedAs("Health")] [Header("Status")] [Range(80, 120)] public int health;

        [FormerlySerializedAs("strenght")] [FormerlySerializedAs("Strenght")] [Range(80, 120)] public int strength;
        [FormerlySerializedAs("Aim")] [Range(80, 120)] public int aim;
        [FormerlySerializedAs("Speed")] [Range(80, 120)] public int speed;

        [FormerlySerializedAs("_interactableCorpsePrefab")] [Header("References")] [SerializeField]
        private GameObject interactableCorpsePrefab;

        [FormerlySerializedAs("_spriteRenderer")] [SerializeField] private SpriteRenderer spriteRenderer;
        [FormerlySerializedAs("_Animator")] [SerializeField] private Animator animator;
        [FormerlySerializedAs("_fxSpeed")] [SerializeField] private float fxSpeed;
        [FormerlySerializedAs("_startColor")] [SerializeField] private Color startColor;
        [FormerlySerializedAs("_damageColor")] [SerializeField] private Color damageColor;
        private Rigidbody2D _body;
        private int _curHealth;
        private bool _hasDefaultWeapon = true;

        private readonly List<Interactable> _interactableList = new List<Interactable>();

        private readonly Dictionary<int, Buff> _buffList = new Dictionary<int, Buff>();

        private bool _canMove;

        
        private bool _canDash = true;
        
        private Coroutine _damageFx;
        private bool _isDashing;
        [SerializeField] UnityEvent damageEvent;
        [SerializeField] UnityEvent cureEvent;
        public int CurHealth
        {
            get => _curHealth;
            set
            {
                if (value > 0)
                    _curHealth = value < health ? value : health;
                else
                    _curHealth = 0;
                UpdateHealth();
            }
        }

        public int OwnerId { get; private set; }

        //------------------------------------ Unity Methods --------------------------------------------
        
        
        private void Start()
        {
            _curHealth = health;
            _body = GetComponent<Rigidbody2D>();
            
            var gunInstance = Instantiate(_defaultGun).GetComponent<Gun>();
            gunInstance.Pick(this, true);
            GameManager.Instance.AddCharacter();
            
            EnableMovement();
            
            transform.position = PlayerController.SActivePlayers[0].Pawn.transform.position;
        }

        private void Update()
        {
            // Atualiza os timers dos buffs
            foreach (var buff in _buffList) buff.Value.UpdateBuff(Time.deltaTime);
            UpdateInteractableList();
        }

        public void PickWeapon(Gun gun, bool defaultFlag)
        {
            equippedGun = gun;
            _hasDefaultWeapon = defaultFlag;
        }

        public void DropWeapon()
        {
            if (_hasDefaultWeapon) return;
            
            equippedGun.Drop(this);
            var gunInstance = Instantiate(_defaultGun).GetComponent<Gun>();
            gunInstance.Pick(this, true);
        }

        //Methods
        public void SetPlayerControlling(PlayerController p)
        {
            OwnerId = p?.PlayerId ?? 0;

            equippedGun?.SetOwner(this);
        }


        public void Interact(int id)
        {
            if (_interactableList.Count != 0)
            {
                _interactableList[0].Interact(this);
            }
        }

        private IEnumerator ChangeColorFx(Color initial, Color final)
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

        private IEnumerator DamageFx()
        {
            yield return StartCoroutine(ChangeColorFx(startColor, damageColor));
            yield return StartCoroutine(ChangeColorFx(damageColor, startColor));
        }

        public void UpdateHealth(int value = 0)
        {
            _curHealth += value;
            if (_damageFx != null)
                StopCoroutine(_damageFx);
            if (value < 0)
            {
                _damageFx = StartCoroutine(DamageFx());
                damageEvent.Invoke();
            }
            else
                cureEvent.Invoke();

            if (_curHealth > health) _curHealth = health;

            if (_curHealth <= 0)
            {
                _curHealth = 0;
                StopAllCoroutines();
                Kill();
            }

            GameEvents.Instance.HealthUpdate(OwnerId, (_curHealth / (float)health));
        }

        public void Fire(Vector2 direction)
        {
            if (!_canMove) return;
            equippedGun.Fire(direction, strength, aim);
        }

        public void ReleaseFire()
        {
            equippedGun.ReleaseFire();
        }


        public void Reload()
        {
            equippedGun.Reload();
        }

        public void SwitchLight()
        {
            equippedGun.SwitchFlashlight();
        }

        public void Move(Vector2 velocity)
        {
            if (!_canMove) return;
            var bodyVelocity = velocity * (BaseSpeed * (speed / 100f));
            if(animator!=null)
                animator.SetBool("isWalking", bodyVelocity.magnitude > 0.01f);
            _body.velocity = bodyVelocity;
        }

        public void ControlRotation(Vector2 direction)
        {
            if (!_canMove) return;
            
            float angle;
            if (direction.x > 0) // virado pra direita
            {
                transform.localScale = new Vector3(1, 1, 1);
                angle = -Vector2.SignedAngle(direction, Vector2.right);
            }
            else // virado pra esquerda
            {
                transform.localScale = new Vector3(-1, 1, 1);
                angle = -Vector2.SignedAngle(direction, Vector2.left);
            }

            if (equippedGun != null) equippedGun.transform.rotation = Quaternion.Euler(0, 0, angle);
        }


        public void AddBuff(Buff buff)
        {
            buff.Owner = this;
            Buff oldBuff;
            
            if (!_buffList.TryGetValue(buff.UniqueId, out oldBuff))
            {
                buff.Grant();
                _buffList.Add(buff.UniqueId, buff);
                GameEvents.Instance.BuffUpdate(OwnerId, buff);
                return;
            }

            // Refreshes buff
            oldBuff.CurTimer = oldBuff.Timer;
            Destroy(buff);
        }

        public void RemoveBuff(Buff buff)
        {
            buff.Remove();
            _buffList.Remove(buff.UniqueId);
        }

        private void Kill()
        {
            var interactableCorpse = Instantiate(interactableCorpsePrefab, transform.position, transform.rotation)
                .GetComponent<InteractableCorpse>();

            interactableCorpse.Initialize(this);
            
            GameManager.Instance.KilLCharacter();
            
            gameObject.SetActive(false);
        }

        public IEnumerator Dash(Vector2 direction)
        {
            if (_canDash == false)
                yield break;

            _canDash = false;
            _isDashing = true;

            var originalSpeed = speed;
            speed = (int)(speed * dashPower);

            trail.emitting = true;
            yield return new WaitForSeconds(dashTime);
            trail.emitting = false;
            _isDashing = false;

            speed = originalSpeed;
            yield return new WaitForSeconds(dashCooldown);
            _canDash = true;
        }

        public void AddInteractable(Interactable interactable)
        {
            interactable.Enter();
            _interactableList.Add(interactable);
            UpdateInteractableList();
        }

        public void RemoveInteractable(Interactable interactable)
        {
            interactable.Exit();
            _interactableList.Remove(interactable);
            UpdateInteractableList();
        }

        public void DisableMovement()
        {
            _canMove = false;
            Move(Vector2.zero);
            ReleaseFire();
        }

        public void EnableMovement()
        {
            _canMove = true;
        }

        private void UpdateInteractableList()
        {
            if (_interactableList.Count != 0)
            {
                var oldInteractable1 = _interactableList[0];
                _interactableList.Sort((x, y) => InteractableCompare(x, y));

                if (oldInteractable1 != _interactableList[0])
                {
                    oldInteractable1.Exit();
                    _interactableList[0].Enter();
                }
            }
        }

        private int InteractableCompare(Interactable x, Interactable y)
        {
            var position = transform.position;
            var distanceX = Vector3.Distance(x.transform.position, position);
            var distanceY = Vector3.Distance(y.transform.position, position);
            return distanceX.CompareTo(distanceY);
        }
    }
}