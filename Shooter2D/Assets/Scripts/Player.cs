using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Character pawn;

    void Possess(Character character)
    {
        pawn = character;
    }

    void Update(){
        if(Input.GetButtonDown("Fire2")){
            pawn.interact();
        }
    }

    void FixedUpdate()
    {
        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");
        
        pawn.move(_x, _y);
    }
}
