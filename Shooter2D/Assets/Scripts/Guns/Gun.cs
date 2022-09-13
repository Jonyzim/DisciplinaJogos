using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    Camera cam;
    [SerializeField] protected float rof;
    public Image reloadImage;
    public Image magazineImage;
    protected float cd = 0;

    [SerializeField] private uint magazine;
    protected uint cur_magazine;

    [SerializeField] private float reloadTime;
    private float reloadProgress = 0;
    protected Vector3 direction;
    private Vector2 mousePos;

    [Header("Teste")]
    [SerializeField] private Sprite magazineSprite;
    [SerializeField] private Sprite reloadSprite;
    [SerializeField] private Sprite backgroundSprite;




    protected abstract void Fire(Vector3 direction);
    protected abstract void ReloadProps(float time);

    protected virtual void Start()
    {
        cam = Camera.main;
        cur_magazine = magazine;
        magazineImage.fillAmount = (float)cur_magazine/(float)magazine;
    }

    protected virtual void Update()
    {
        magazineImage.fillAmount = (float)cur_magazine/(float)magazine;

        if(cur_magazine == 0 && cd <= 0){
            ReloadProps(reloadTime);
            reloadProgress += Time.deltaTime;
            reloadImage.fillAmount = reloadProgress/reloadTime;
            if(reloadProgress > reloadTime){
                reloadProgress = 0;
                cur_magazine = magazine;
                reloadImage.fillAmount = 0;
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
