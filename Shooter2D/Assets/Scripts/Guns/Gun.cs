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
    public uint cur_magazine;

    [SerializeField] private float reloadTime;
    [SerializeField] protected Transform spawnTransf;

    //Substituir quando equipar arma
    [Header("Sprites")]
    [SerializeField] private Sprite magazineSprite;
    [SerializeField] private Sprite reloadSprite;
    [SerializeField] private Sprite backgroundSprite;
    private float reloadProgress = 0;
    [SerializeField] private GameObject interactableReference;

    [HideInInspector] protected int ownerId;


    public void pick(Character character){
        SetOwner(character);
        transform.parent = character.gameObject.transform;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        character.gun = this;
        GameEvents.current.PickWeapon(1, magazineSprite, backgroundSprite);
        GameEvents.current.MagazineUpdate(1, (float)cur_magazine/(float)magazine);
    }
    public void drop(Character character){
        RemoveOwner(character);
        GameObject instance = Instantiate(interactableReference);

        
        instance.GetComponent<GunInteractable>().gun = gameObject;

        gameObject.transform.parent = instance.transform;
    }

    protected virtual void Fire(Vector3 direction){
        FireProps();
        cd = 1/rof;
        cur_magazine -= 1;
        GameEvents.current.MagazineUpdate(1, (float)cur_magazine/(float)magazine);
    }
    protected abstract void FireProps();
    protected abstract void ReloadProps(float time);

    public void SetOwner(Character character)
    {
        ownerId = character.character_id;
        print("Player " + ownerId + " got a " + gameObject.name);
        character.onFire += Fire;
        character.onReload += Teste;
    }

    public void RemoveOwner(Character character){
        character.onFire -= Fire;
        character.onReload -= Teste;
    }

    protected virtual void Start()
    {
        cur_magazine = magazine;

        //Caso a arma já esteja equipada antes do jogo começar
        Character character = gameObject.GetComponentInParent<Character>();
        if(character != null){
            SetOwner(character);

            GameEvents.current.PickWeapon(1, magazineSprite, backgroundSprite);
            GameEvents.current.MagazineUpdate(1, (float)cur_magazine/(float)magazine);
        }
    }

    protected virtual void Update()
    {

        if(cur_magazine == 0 && cd <= 0){
            Reload();
        }
        else {
            reloadProgress = 0;
        }
        cd -= Time.deltaTime;        
    }

    void Reload(){

        //Não recarregar caso munição esteja cheia
        if(!(cur_magazine == magazine)){
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
    }

    void Teste(){
        cur_magazine = 0;
    }
}