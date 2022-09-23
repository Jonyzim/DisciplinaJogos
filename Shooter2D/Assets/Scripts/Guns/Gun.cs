using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public abstract class Gun : MonoBehaviour
{
    [Header("General")]
    [SerializeField] protected float rof;
    protected float cd = 0;

    [SerializeField] private uint _magazine;
    public uint curMagazine;

    [SerializeField] private float _reloadTime;
    [SerializeField] protected Transform spawnTransf;

    //Substituir quando equipar arma
    [Header("Sprites")]
    [SerializeField] private Sprite _magazineSprite;
    [SerializeField] private Sprite _reloadSprite;
    [SerializeField] private Sprite _backgroundSprite;
    private float _reloadProgress = 0;
    [SerializeField] private GameObject interactableReference;
    [SerializeField] private Light2D _flashLight;

    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float spread;

    protected int ownerId;

    public void Pick(Character character)
    {
        SetOwner(character);
        transform.parent = character.gameObject.transform;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        character.gun = this;
        GameEvents.s_instance.PickWeapon(ownerId, _magazineSprite, _backgroundSprite);
        GameEvents.s_instance.MagazineUpdate(ownerId, (float)curMagazine / (float)_magazine);
    }
    public void Drop(Character character)
    {
        RemoveOwner(character);
        GameObject instance = Instantiate(interactableReference);


        instance.GetComponent<GunInteractable>().gun = gameObject;

        gameObject.transform.parent = instance.transform;
    }

    public virtual void Fire(Vector2 direction, int strenght, int aim)
    {
        FireProps();
        cd = 1 / rof;
        curMagazine -= 1;
        GameEvents.s_instance.MagazineUpdate(ownerId, (float)curMagazine / (float)_magazine);

        //Calculate new spread based on character Aim stat
        float _spread = aim > 100 ? (spread * (100 / ((aim * 2) - 100))) : (spread + 100 - aim);

        Vector2 _direction = Quaternion.AngleAxis(-Random.Range(-_spread, _spread), new Vector3(0, 0, 1)) * direction;

        GameObject _bullet = Instantiate(bullet, spawnTransf.transform.position, Quaternion.Euler(0, 0, 0));
        Bullet bulletScript = _bullet.GetComponent<Bullet>();
        bulletScript.SetVariables(_direction, strenght);
        bulletScript.SetPlayer(ownerId);
    }
    public abstract void ReleaseFire();
    protected abstract void FireProps();
    protected abstract void ReloadProps(float time);

    public void SwitchFlashlight()
    {
        if (_flashLight != null)
        {
            _flashLight.enabled = !_flashLight.enabled;
        }
    }

    public void SetOwner(Character character)
    {
        ownerId = character.character_id;
        print("Player " + ownerId + " got a " + gameObject.name);
    }

    public void RemoveOwner(Character character)
    {
        print("Player " + ownerId + " dropped a " + gameObject.name);
        ownerId = -1;
    }

    protected virtual void Start()
    {
        curMagazine = _magazine;

        //Caso a arma já esteja equipada antes do jogo começar
        Character character = gameObject.GetComponentInParent<Character>();
        if (character != null)
        {
            Pick(character);
        }
    }

    protected virtual void Update()
    {

        if (curMagazine == 0 && cd <= 0)
        {
            ReloadUpdate();
        }
        else
        {
            _reloadProgress = 0;
        }
        cd -= Time.deltaTime;
    }

    void ReloadUpdate()
    {

        //Não recarregar caso munição esteja cheia
        if (!(curMagazine == _magazine))
        {
            ReloadProps(_reloadTime);
            _reloadProgress += Time.deltaTime;
            GameEvents.s_instance.ReloadUpdate(ownerId, _reloadProgress / _reloadTime);
            if (_reloadProgress > _reloadTime)
            {
                _reloadProgress = 0;
                curMagazine = _magazine;
                GameEvents.s_instance.ReloadUpdate(ownerId, 0);
                GameEvents.s_instance.MagazineUpdate(ownerId, (float)curMagazine / (float)_magazine);
            }
        }
    }

    public void Reload()
    {
        curMagazine = 0;
    }
}