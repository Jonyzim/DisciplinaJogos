using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    Camera cam;
    [SerializeField] private float rof;
    public Image reloadImage;
    private float cd = 0;

    [SerializeField] private uint magazine;
    private uint cur_magazine;

    [SerializeField] private float reloadTime;
    private float reloadProgress = 0;

    [SerializeField] protected GameObject bullet;
    private Vector3 direction;

    Vector2 mousePos;


    protected void Fire(Vector3 direction){
        GameObject _bullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
        _bullet.GetComponent<Bullet>().SetDirection(direction);
    }

    void Start()
    {
        cam = Camera.main;
        cur_magazine = magazine;
    }

    void Update()
    {
        if(cur_magazine == 0){
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

        if (Input.GetButton("Fire1") && cur_magazine > 0)
        {
            if(cd <= 0){
                Fire(direction);
                cd = 1/rof;
                cur_magazine -= 1;
            }
        }
    }
}
