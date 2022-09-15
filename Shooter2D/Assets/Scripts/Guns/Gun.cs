using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    [Header("General")]
    [SerializeField] protected float rof;
    protected float cd = 0;

    [SerializeField] private uint magazine;
    protected uint cur_magazine;

    [SerializeField] private float reloadTime;

    //Substituir quando equipar arma
    [Header("Sprites")]
    [SerializeField] private Sprite magazineSprite;
    [SerializeField] private Sprite reloadSprite;
    [SerializeField] private Sprite backgroundSprite;
    private float reloadProgress = 0;



    public void drop(){

    }

    protected virtual void Fire(Vector3 direction){
        FireProps();
        cd = 1/rof;
        cur_magazine -= 1;
        GameEvents.current.MagazineUpdate(1, (float)cur_magazine/(float)magazine);
    }
    protected abstract void FireProps();
    protected abstract void ReloadProps(float time);

    public void SetOwner(Character character){
        character.onFire += Fire;
    }

    public void RemoveOwner(Character character){
        character.onFire -= Fire;
    }

    protected virtual void Start()
    {
        cur_magazine = magazine;

        SetOwner(gameObject.GetComponentInParent<Character>());

        GameEvents.current.PickWeapon(1, magazineSprite, backgroundSprite);
        GameEvents.current.MagazineUpdate(1, (float)cur_magazine/(float)magazine);
    }

    protected virtual void Update()
    {

        if(cur_magazine == 0 && cd <= 0){
            ReloadProps(reloadTime);
            reloadProgress += Time.deltaTime;
            GameEvents.current.ReloadUpdate(1, reloadProgress/reloadTime);
            if(reloadProgress > reloadTime){
                reloadProgress = 0;
                cur_magazine = magazine;
                GameEvents.current.ReloadUpdate(1, 0);
                GameEvents.current.MagazineUpdate(1, (float)cur_magazine/(float)magazine);
            }
        }
        else {
            reloadProgress = 0;
        }

        cd -= Time.deltaTime;        
    }
}
