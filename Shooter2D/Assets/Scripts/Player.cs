using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Character pawn;
    private Camera cam;
    private Vector3 direction;
    private Vector2 mousePos;
    private Vector2 center;

    void Possess(Character character)
    {
        pawn = character;
    }

    void Start(){
        cam = Camera.main;
    }

    //Consertar o centro para ser o centro da arma
    void Update(){
        center = transform.position;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        direction = (mousePos - center).normalized;

        if(Input.GetButton("Fire1")){
            pawn.Fire(direction);
        }
        if(Input.GetButtonDown("Fire2")){
            pawn.Interact();
        }
    }

    void FixedUpdate()
    {
        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");
        
        pawn.Move(_x, _y);
    }
}
