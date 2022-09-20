using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    [SerializeField] private Character pawn;
    private Camera cam;
    private Vector3 direction;
    private Vector2 mousePos;
    private Vector2 center;
    private Vector2 movement = new Vector2(0, 0);
    private int score;
    [SerializeField] TMP_Text playerScoreView;


    void Possess(Character character)
    {
        pawn = character;
        pawn.SetPlayerControlling(this);
    }

    public void AddScore(int n)
    {
        score += n;
        playerScoreView.text = score.ToString().PadLeft(10, '0');
    }
    void Start(){
        cam = Camera.main;
        if (pawn != null)
        {
            pawn.SetPlayerControlling(this);
        }
    }

    //Consertar o centro para ser o centro da arma
    void Update(){
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
            Debug.Log("Reload");
            pawn.Reload();
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        pawn.Move(movement.x, movement.y);
    }
}
