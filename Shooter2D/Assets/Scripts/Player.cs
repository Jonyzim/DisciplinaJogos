using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private CharacterMovement pawn;

    void Possess(CharacterMovement character){
        pawn = character;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");
        
        pawn.move(_x, _y);
    }
}
