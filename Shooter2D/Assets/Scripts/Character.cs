using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public event Action onInteract;
    public void interact(){
        if(onInteract != null){
            onInteract();
        }
    }


    

    [SerializeField] float speed;

    Camera cam;
    [SerializeField] private Gun gun;
    private Vector3 direction;
    private Quaternion lookRotation;
    float angle;

    [SerializeField] private float rotationSpeed;

    public void move(float x, float y){
        transform.position += new Vector3(x, y, 0)*speed;
    }

    void Start(){
        cam = Camera.main;
    }
    void Update(){
        controlRotation();
    }

    private void controlRotation(){
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 center = transform.position;

        direction = (mousePos - center).normalized;

        
        if (direction.x > 0) // virado pra direita
        {
            transform.localScale = new Vector3(1, 1, 1);
            angle = -Vector2.SignedAngle(direction,  Vector2.right);
        }
        else // virado pra esquerda
        {
            transform.localScale = new Vector3(-1, 1, 1);
            angle = -Vector2.SignedAngle(direction, Vector2.left);
        }

        lookRotation = Quaternion.Euler(0,0,angle);
        gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
