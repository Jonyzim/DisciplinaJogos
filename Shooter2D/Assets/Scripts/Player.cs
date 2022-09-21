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
    private int score;
    [SerializeField] private int playerId;
    public int PlayerId{
        get {return playerId;}
    }


    void Possess(Character character)
    {
        pawn = character;
        pawn.SetPlayerControlling(this);
    }

    void Awake(){
        cam = Camera.main;
        if (pawn != null)
        {
            pawn.SetPlayerControlling(this);
        }
    }

    //Consertar o centro para ser o centro da arma
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            GameEvents.current.Pause(PlayerId);
        }

        if (HUDManager.IsPaused)
            return;

        center = pawn.transform.position;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        direction = (mousePos - center).normalized;

        if(Input.GetButton("Fire1")){
            pawn.Fire(direction);
        }
        if(Input.GetButtonDown("Fire2")){
            pawn.Interact(pawn.character_id);
        }
        if(Input.GetButtonDown("Reload")){
            pawn.Reload();
        }


        //!Temporary
        if(Input.GetButtonDown("Fire3")){
            pawn.SwitchLight();
            //GameEvents.current.WaveChange();
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        pawn.Move(movement.x, movement.y);
    }
}
