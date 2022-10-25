using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public abstract class Gun : MonoBehaviour
{
    public uint BulletCost => _bulletCost;

    [Header("General")]

    [SerializeField] protected Transform SpawnTransf;
    [SerializeField] protected GameObject Bullet;
    [SerializeField] private GameObject _interactableReference;
    [SerializeField] private Light2D _flashLight;

    [Header("GunStats")]
    [SerializeField] protected float Rof;
    [SerializeField] protected float Spread;
    [SerializeField] private int _damage;
    [SerializeField] private float _reloadTime;
    [SerializeField] private uint _clip;

    [Tooltip("0 = Infinite bullets")][SerializeField] private uint _maxAmmo;

    [Header("VFX")]
    [SerializeField] protected Sound ShotSFX;

    // Substituir quando equipar arma
    [Header("Sprites")]
    [SerializeField] private Sprite _magazineSprite;
    [SerializeField] private Sprite _reloadSprite;
    [SerializeField] private Sprite _backgroundSprite;

    protected uint _curClip;
    protected float Cd = 0;
    protected int OwnerId;

    private uint _bulletCost;
    private uint _curAmmo;
    private float _reloadProgress = 0;

    // Methods
    public void Pick(Character character)
    {
        _flashLight.enabled = true;
        SetOwner(character);
        transform.parent = character.gameObject.transform;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        character.EquippedGun = this;
        GameEvents.Instance.PickWeapon(OwnerId, _magazineSprite, _backgroundSprite);
        GameEvents.Instance.MagazineUpdate(OwnerId, (float)_curClip / (float)_clip);
        GameEvents.Instance.AmmoUpdate(OwnerId, _curAmmo, _maxAmmo);
    }

    public void Drop(Character character)
    {
        _flashLight.enabled = false;
        RemoveOwner(character);
        GameObject instance = Instantiate(_interactableReference, gameObject.transform.position, gameObject.transform.rotation);


        instance.GetComponent<InteractableGun>().NewGun = gameObject;

        gameObject.transform.parent = instance.transform;
    }

    public virtual Bullet Fire(Vector2 direction, int strenght, float aim)
    {
        FireProps();
        Cd = 1 / Rof;
        GameEvents.Instance.MagazineUpdate(OwnerId, (float)_curClip / (float)_clip);

        // Calculate new spread based on character Aim stat
        float _spread = aim > 100 ? (Spread * (100 / ((aim * 2) - 100))) : (Spread + 100 - aim);

        Vector2 _direction = Quaternion.AngleAxis(-Random.Range(-_spread, _spread), new Vector3(0, 0, 1)) * direction;

        GameObject _bullet = Instantiate(Bullet, SpawnTransf.transform.position, Quaternion.Euler(0, 0, 0));
        Bullet bulletScript = _bullet.GetComponent<Bullet>();
        bulletScript.SetPlayer(OwnerId);
        bulletScript.SetVariables(_direction, strenght, _damage);
        return bulletScript;
    }

    public void SwitchFlashlight()
    {
        if (_flashLight != null)
        {
            _flashLight.enabled = !_flashLight.enabled;
        }
    }

    public void SetOwner(Character character)
    {
        OwnerId = character.OwnerId;
    }

    public void RemoveOwner(Character character)
    {
        OwnerId = -1;
    }

    /// <summary>
    /// Zera clip para recarregar balas
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
        if (_maxAmmo != 0)
        {
            _curAmmo = _maxAmmo - _curClip;
            GameEvents.Instance.AmmoUpdate(OwnerId, _curAmmo, _maxAmmo);
        }
    }

    public float GetAmmunitionPercentage()
    {
        if (_maxAmmo != 0)
        {
            return (float)(_curAmmo + _curClip) / _maxAmmo;
        }
        return 1f;
    }

    public abstract void ReleaseFire();

    protected virtual void FireProps()
    {
        ShotSFX.Play();
    }

    protected abstract void ReloadProps(float time);

    private void ReloadUpdate()
    {
        if (_curAmmo > 0 || _maxAmmo == 0)
        {
            _reloadProgress += Time.deltaTime;
            GameEvents.Instance.ReloadUpdate(OwnerId, _reloadProgress / _reloadTime);

            if (_reloadProgress > _reloadTime)
            {
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


                GameEvents.Instance.ReloadUpdate(OwnerId, 0);
                GameEvents.Instance.MagazineUpdate(OwnerId, (float)_curClip / (float)_clip);
                GameEvents.Instance.AmmoUpdate(OwnerId, _curAmmo, _maxAmmo);
            }
        }
    }

    //Unity Methods
    protected virtual void Start()
    {
        _curClip = _clip;
        _curAmmo = _maxAmmo - _curClip;

        //Caso a arma já esteja equipada antes do jogo começar
        Character character = gameObject.GetComponentInParent<Character>();
        if (character != null)
        {
            Pick(character);
        }
    }

    protected virtual void Update()
    {

        if (_curClip == 0 && Cd <= 0)
        {
            ReloadUpdate();
        }
        else
        {
            _reloadProgress = 0;
        }
        Cd -= Time.deltaTime;
    }

}