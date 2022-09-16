using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Character pawn;
    private Vector2 movement = new Vector2(0, 0);
    void Possess(Character character)
    {
        pawn = character;
    }

    void Update(){
        if(Input.GetButtonDown("Fire2")){
            pawn.interact();
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        
        pawn.move(movement.x, movement.y);
    }
}
