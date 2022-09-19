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
    private Vector2 movement = new Vector2(0, 0);
    
    void Possess(Character character)
    {
        pawn = character;
    }

    void Start(){
        cam = Camera.main;
    }

    [SerializeField] private bool isJoystick = false;
    //Consertar o centro para ser o centro da arma
    void Update(){
        if (isJoystick)
        {

            Vector2 oldDir = direction;
            direction.x = -Input.GetAxis("RightVertical");
            direction.y = -Input.GetAxis("RightHorizontal");
            if (direction.magnitude < 0.75f)
            {
                direction = oldDir;
            }
        }
        else
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 center = transform.position;
            direction = (mousePos - center).normalized;
        }

        if (Input.GetButton("Fire1") || Input.GetAxis("Fire1")!=0)
        {
            pawn.Fire(direction);
        }
        if(Input.GetButtonDown("Fire2")){
            pawn.Interact(pawn.character_id);
        }
        if(Input.GetButtonDown("Reload")){
            Debug.Log("Reload");
            pawn.Reload();
        }

        pawn.ControlRotation(direction);

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        pawn.Move(movement.x, movement.y);
    }
}
