using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public abstract class Gun : MonoBehaviour
{
    [Header("General")]

    protected uint CurMagazine;
    [SerializeField] protected Transform SpawnTransf;
    [SerializeField] protected GameObject Bullet;
    protected float cd = 0;
    protected int OwnerId;
    [SerializeField] private GameObject _interactableReference;
    [SerializeField] private Light2D _flashLight;

    [Header("GunStats")]
    [SerializeField] protected float Rof;
    [SerializeField] protected float Spread;
    [SerializeField] private int _damage;
    [SerializeField] private float _reloadTime;
    [SerializeField] private uint _magazine;

    //Substituir quando equipar arma
    [Header("Sprites")]
    [SerializeField] private Sprite _magazineSprite;
    [SerializeField] private Sprite _reloadSprite;
    [SerializeField] private Sprite _backgroundSprite;

    private float _reloadProgress = 0;

    //Methods
    public void Pick(Character character)
    {
        SetOwner(character);
        transform.parent = character.gameObject.transform;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        character.EquippedGun = this;
        GameEvents.s_Instance.PickWeapon(OwnerId, _magazineSprite, _backgroundSprite);
        GameEvents.s_Instance.MagazineUpdate(OwnerId, (float)CurMagazine / (float)_magazine);
    }
    public void Drop(Character character)
    {
        RemoveOwner(character);
        GameObject instance = Instantiate(_interactableReference, gameObject.transform.position, gameObject.transform.rotation);


        instance.GetComponent<GunInteractable>().Gun = gameObject;

        gameObject.transform.parent = instance.transform;
    }

    public virtual void Fire(Vector2 direction, int strenght, float aim)
    {
        FireProps();
        cd = 1 / Rof;
        GameEvents.s_Instance.MagazineUpdate(OwnerId, (float)CurMagazine / (float)_magazine);

        //Calculate new spread based on character Aim stat
        float _spread = aim > 100 ? (Spread * (100 / ((aim * 2) - 100))) : (Spread + 100 - aim);

        Vector2 _direction = Quaternion.AngleAxis(-Random.Range(-_spread, _spread), new Vector3(0, 0, 1)) * direction;

        GameObject _bullet = Instantiate(Bullet, SpawnTransf.transform.position, Quaternion.Euler(0, 0, 0));
        Bullet bulletScript = _bullet.GetComponent<Bullet>();
        bulletScript.SetPlayer(OwnerId);
        bulletScript.SetVariables(_direction, strenght, _damage);
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
        OwnerId = character.CharacterId;
    }
    public void RemoveOwner(Character character)
    {
        OwnerId = -1;
    }
    public void Reload()
    {
        //Não recarregar caso munição esteja cheia
        if(CurMagazine < _magazine){
            CurMagazine = 0;
        }
    }
    public abstract void ReleaseFire();
    protected abstract void FireProps();

    protected abstract void ReloadProps(float time);




    private void ReloadUpdate()
    {
        ReloadProps(_reloadTime);
        _reloadProgress += Time.deltaTime;
        GameEvents.s_Instance.ReloadUpdate(OwnerId, _reloadProgress / _reloadTime);
        if (_reloadProgress > _reloadTime)
        {
            _reloadProgress = 0;
            CurMagazine = _magazine;
            GameEvents.s_Instance.ReloadUpdate(OwnerId, 0);
            GameEvents.s_Instance.MagazineUpdate(OwnerId, (float)CurMagazine / (float)_magazine);
        }
    }

    //Unity Methods
    protected virtual void Start()
    {
        CurMagazine = _magazine;

        //Caso a arma já esteja equipada antes do jogo começar
        Character character = gameObject.GetComponentInParent<Character>();
        if (character != null)
        {
            Pick(character);
        }
    }

    protected virtual void Update()
    {

        if (CurMagazine == 0 && cd <= 0)
        {
            ReloadUpdate();
        }
        else
        {
            _reloadProgress = 0;
        }
        cd -= Time.deltaTime;
    }

}