using FMODUnity;
using MWP.Guns.Bullets;
using MWP.Interactables;
using MWP.Misc;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace MWP.Guns
{
    [DisallowMultipleComponent]
    public abstract class Gun : MonoBehaviour
    {
        [Header("General")] [SerializeField] protected Transform SpawnTransf;

        [SerializeField] protected GameObject Bullet;
        [SerializeField] private GameObject _interactableReference;
        [SerializeField] private Light2D _flashLight;

        [Header("GunStats")] [SerializeField] protected float Rof;

        [SerializeField] protected float Spread;
        [SerializeField] private int _damage;
        [SerializeField] private float _reloadTime;
        [SerializeField] private uint _clip;

        [Tooltip("0 = Infinite bullets")] [SerializeField]
        private uint _maxAmmo;

        [Header("SFX")] [SerializeField] protected EventReference ShotSfxEvent;

        // Substituir quando equipar arma
        [Header("Sprites")] [SerializeField] private Sprite _magazineSprite;

        [SerializeField] private Sprite _reloadSprite;
        [SerializeField] private Sprite _backgroundSprite;

        private uint _curAmmo;

        protected uint _curClip;
        private float _reloadProgress;
        protected float Cd;
        private int _ownerId;
        public uint bulletCost;

        //Unity Methods
        protected virtual void Start()
        {
            _curClip = _clip;
            _curAmmo = _maxAmmo - _curClip;

            //Caso a arma já esteja equipada antes do jogo começar
            var character = gameObject.GetComponentInParent<Character>();
            if (character != null) Pick(character);
        }

        protected virtual void Update()
        {
            if (_curClip == 0 && Cd <= 0)
                ReloadUpdate();
            else
                _reloadProgress = 0;
            Cd -= Time.deltaTime;
        }

        // Methods
        public void Pick(Character character, bool defaultFlag = false)
        {
            _flashLight.enabled = true;
            SetOwner(character);
            var transf = transform;
            
            character.PickWeapon(this, defaultFlag);
            
            transf.parent = character.weaponSlot.transform;
            transf.localPosition = new Vector3(0, 0.75f, 0);
            transf.localScale = Vector3.one;
            
            GameEvents.Instance.PickWeapon(_ownerId, _magazineSprite, _backgroundSprite);
            GameEvents.Instance.MagazineUpdate(_ownerId, _curClip / (float)_clip);
            GameEvents.Instance.AmmoUpdate(_ownerId, _curAmmo, _maxAmmo);
        }

        public void Drop(Character character)
        {
            ReleaseFire();
            _flashLight.enabled = false;
            RemoveOwner(character);
            if (_interactableReference != null)
            {
                var instance = Instantiate(_interactableReference, gameObject.transform.position,
                    gameObject.transform.rotation);


                instance.GetComponent<InteractableGun>().newGun = gameObject;

                gameObject.transform.parent = instance.transform;
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        public virtual Bullet Fire(Vector2 direction, int strength, float aim)
        {
            FireProps();
            Cd = 1 / Rof;
            GameEvents.Instance.MagazineUpdate(_ownerId, _curClip / (float)_clip);

            // Calculate new spread based on character Aim stat
            var spread = aim > 100 ? Spread * (100 / (aim * 2 - 100)) : Spread + 100 - aim;

            direction = Quaternion.AngleAxis(-Random.Range(-spread, spread), new Vector3(0, 0, 1)) *
                                 direction;

            var bullet = Instantiate(Bullet, SpawnTransf.transform.position, Quaternion.Euler(0, 0, 0));
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetPlayer(_ownerId);
            bulletScript.SetGun(this);
            bulletScript.SetVariables(direction, strength, _damage);
            return bulletScript;
        }

        public void SwitchFlashlight()
        {
            if (_flashLight != null) _flashLight.enabled = !_flashLight.enabled;
        }

        public void SetOwner(Character character)
        {
            _ownerId = character.OwnerId;
        }

        public void RemoveOwner(Character character)
        {
            _ownerId = -1;
        }

        /// <summary>
        ///     Zera clip para recarregar balas
        /// </summary>
        public void Reload()
        {
            //Não recarregar caso munição esteja cheia
            if (_curClip < _clip)
            {
                _curAmmo += _curClip;
                _curClip = 0;
            }
        }

        public void RechargeAmmunition()
        {
            if (_maxAmmo == 0) return;
            _curAmmo = _maxAmmo - _curClip;
            ReleaseFire();
            GameEvents.Instance.AmmoUpdate(_ownerId, _curAmmo, _maxAmmo);
        }

        public float GetAmmunitionPercentage()
        {
            if (_maxAmmo != 0) return (float)(_curAmmo + _curClip) / _maxAmmo;
            return 1f;
        }

        public abstract void ReleaseFire();

        protected virtual void FireProps()
        {
            RuntimeManager.PlayOneShot(ShotSfxEvent, transform.position);
        }

        protected abstract void ReloadProps(float time);

        private void ReloadUpdate()
        {
            if (_curAmmo <= 0 && _maxAmmo != 0) return;
            if(_reloadProgress==0)
                ReleaseFire();
            _reloadProgress += Time.deltaTime;
            GameEvents.Instance.ReloadUpdate(_ownerId, _reloadProgress / _reloadTime);
            if (!(_reloadProgress > _reloadTime)) return;
            ReloadProps(_reloadTime);
            _reloadProgress = 0;
            if (_curAmmo > _clip)
            {
                _curClip = _clip;
                _curAmmo -= _clip;
            }
            else
            {
                _curClip = _curAmmo;
                _curAmmo = 0;
            }


            GameEvents.Instance.ReloadUpdate(_ownerId, 0);
            GameEvents.Instance.MagazineUpdate(_ownerId, _curClip / (float)_clip);
            GameEvents.Instance.AmmoUpdate(_ownerId, _curAmmo, _maxAmmo);
        }
    }
}