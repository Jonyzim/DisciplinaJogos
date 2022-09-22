using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] private Character pawn;
    private Camera cam;
    private Vector2 direction;
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

    // ---------------------- Old Input System --------------------------
    //Consertar o centro para ser o centro da arma
    // void Update()
    // {
    //     if (Input.GetButtonDown("Cancel"))
    //     {
    //         GameEvents.current.Pause(PlayerId);
    //     }

    //     if (HUDManager.IsPaused)
    //         return;

    //     center = pawn.transform.position;
    //     mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    //     direction = (mousePos - center).normalized;

    //     if(Input.GetButton("Fire1")){
    //         pawn.Fire(direction);
    //     }
    //     if(Input.GetButtonDown("Fire2")){
    //         pawn.Interact(pawn.character_id);
    //     }
    //     if(Input.GetButtonDown("Reload")){
    //         pawn.Reload();
    //     }


    //     //!Temporary
    //     if(Input.GetButtonDown("Fire3")){
    //         pawn.SwitchLight();
    //         //GameEvents.current.WaveChange();
    //     }

    ////     movement.x = Input.GetAxisRaw("Horizontal");
    ////     movement.y = Input.GetAxisRaw("Vertical");
    // }
    // ----------------------------------------------------------------------

    public void OnMove(InputAction.CallbackContext context){
        movement = context.ReadValue<Vector2>();
    }
    public void OnPause(InputAction.CallbackContext context){
        if(context.performed){
            GameEvents.current.Pause(PlayerId);
        }
    }
    public void OnFire(InputAction.CallbackContext context){
        if(context.performed){
            pawn.Fire(direction);
        }
    }
    public void OnReload(InputAction.CallbackContext context){
        if(context.performed){
            pawn.Reload();
        }
    }
    public void OnInteract(InputAction.CallbackContext context){
        if(context.performed){
            pawn.Interact(pawn.character_id);
        }
    }
    public void OnFlashLight(InputAction.CallbackContext context){
        if(context.performed){
            pawn.SwitchLight();
        }
    }
    public void OnAim(InputAction.CallbackContext context){
        direction = ((Vector2)cam.ScreenToWorldPoint(context.ReadValue<Vector2>()) - (Vector2)pawn.transform.position).normalized;
        Debug.Log(direction);
        pawn.ControlRotation(direction);
    }
    void FixedUpdate()
    {
        pawn.Move(movement);
    }
}
