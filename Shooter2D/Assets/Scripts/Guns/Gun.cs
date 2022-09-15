using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    Camera cam;

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
    protected Vector3 direction;
    private Vector2 mousePos;


    public void drop(){

    }

    protected virtual void Fire(Vector3 direction){
        FireProps();
        cd = 1/rof;
        cur_magazine -= 1;
        GameEvents.current.magazineUpdate(1, (float)cur_magazine/(float)magazine);
    }
    protected abstract void FireProps();
    protected abstract void ReloadProps(float time);

    protected virtual void Start()
    {
        cam = Camera.main;
        cur_magazine = magazine;

        GameEvents.current.pickWeapon(1, magazineSprite, backgroundSprite);
        GameEvents.current.magazineUpdate(1, (float)cur_magazine/(float)magazine);
    }

    protected virtual void Update()
    {

        if(cur_magazine == 0 && cd <= 0){
            ReloadProps(reloadTime);
            reloadProgress += Time.deltaTime;
            GameEvents.current.reloadUpdate(1, reloadProgress/reloadTime);
            if(reloadProgress > reloadTime){
                reloadProgress = 0;
                cur_magazine = magazine;
                GameEvents.current.reloadUpdate(1, 0);
                GameEvents.current.magazineUpdate(1, (float)cur_magazine/(float)magazine);
            }
        }
        else {
            reloadProgress = 0;
        }

        cd -= Time.deltaTime;

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 center = transform.position;

        direction = (mousePos - center).normalized;
        
    }
}
