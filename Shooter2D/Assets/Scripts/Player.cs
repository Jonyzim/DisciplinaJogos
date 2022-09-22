using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public static Player[] activePlayers = new Player[4];

    [SerializeField] private Character pawn;


    [SerializeField] private GameObject HUDPrefab;
    //TEMPORARY
    
    [SerializeField] private GameObject CharacterPrefab;

    private Camera cam;
    private Vector2 direction = new Vector2(1, 0);
    private Vector2 movement = new Vector2(0, 0);
    private int score;
    private int playerId;
    public int PlayerId{
        get {return playerId;}
    }

    private bool _isFiring = false;


    public void Possess(Character character)
    {
        if(character != null){
            pawn = character;
            pawn.SetPlayerControlling(this);
        }
    }

    void Start(){
        cam = Camera.main;

        for(int i = 0; i < 4; i++){
            if(activePlayers[i] == null){
                playerId = i+1;
                activePlayers[i] = this;
                break;
            }
        }

        Instantiate(HUDPrefab).GetComponentInChildren<HUDManager>().SetupHUD(playerId);

        //TEMPORARY, Change to character selection instead
        Possess(Instantiate(CharacterPrefab).GetComponent<Character>());

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
            _isFiring = true;
        }
        else if(context.canceled){
            _isFiring = false;
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
        if(context.control.device.name == "Mouse"){
            direction = ((Vector2)cam.ScreenToWorldPoint(context.ReadValue<Vector2>()) - (Vector2)pawn.transform.position).normalized;
        }
        else{
            direction = context.ReadValue<Vector2>();
        }
        pawn.ControlRotation(direction);
    }
    void FixedUpdate()
    {
        pawn.Move(movement);
        if(_isFiring){
            pawn.Fire(direction);
        }
        else{
            pawn.ReleaseFire();
        }
    }

    void OnDestroy(){
        pawn.SetPlayerControlling(null);
        activePlayers[playerId-1] = null;
    }
}
