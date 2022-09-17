using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe futuramente abstrata, serão utilizados as classes CowCharacter, PigCharacter e etc

public class Character : MonoBehaviour
{
    private int _character_id = 0;

    public int character_id
    {
        get { return _character_id; }
    }
    public event Action onInteract;
    public void Interact(){
        if(onInteract != null){
            onInteract();
        }
    }

    public event Action<Vector3> onFire;
    public void Fire(Vector3 direction){
        if(onFire != null){
            onFire(direction);
        }
    }


    

    [SerializeField] float speed;

    Camera cam;
    [SerializeField] private Gun gun;
    private Vector3 direction;
    private Quaternion lookRotation;
    float angle;

    public void Move(float x, float y){
        transform.position += new Vector3(x, y, 0)*speed;
    }

    void Start(){
        cam = Camera.main;
    }
    void Update(){
        ControlRotation();
    }

    private void ControlRotation(){
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
        gun.transform.rotation = lookRotation;
    }
}
