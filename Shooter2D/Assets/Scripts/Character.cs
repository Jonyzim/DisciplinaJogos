using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe futuramente abstrata, serão utilizados as classes CowCharacter, PigCharacter e etc

public class Character : MonoBehaviour
{

    AudioSource test;
    private int _character_id;
    public int character_id
    {
        get { return _character_id; }
    }

    Rigidbody2D body;
    private float baseSpeed = 10;
    [SerializeField] public Gun gun;


    public void SetPlayerControlling(Player p)
    {
        if (p != null)
        {
            _character_id = p.PlayerId;
        }
        else
            _character_id = 0;
        gun.SetOwner(this);
    }


    [Header("Status")]
    [Range(80, 120)] public int Health;
    [Range(80, 120)] public int Strenght;
    [Range(80, 120)] public int Speed;
    [Range(80, 120)] public int Aim;

    //Custom Events
    public event Action<int> onInteract;
    public void Interact(int id)
    {
        if (onInteract != null)
        {
            onInteract(id);
        }
    }

    public void Fire(Vector2 direction)
    {
        gun.Fire(direction, Strenght, Aim);
    }

    public void ReleaseFire()
    {
        gun.ReleaseFire();
    }


    public void Reload()
    {
        gun.Reload();
    }
    public void SwitchLight()
    {
        gun.SwitchFlashlight();
    }


    //Engine Methods
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //ControlRotation();
    }

    //Methods
    public void Move(Vector2 velocity)
    {
        body.velocity = velocity * baseSpeed * (Speed / 100);
    }
    public void ControlRotation(Vector2 direction)
    {
        float angle;
        if (direction.x > 0) // virado pra direita
        {
            transform.localScale = new Vector3(1, 1, 1);
            angle = -Vector2.SignedAngle(direction, Vector2.right);
        }
        else // virado pra esquerda
        {
            transform.localScale = new Vector3(-1, 1, 1);
            angle = -Vector2.SignedAngle(direction, Vector2.left);
        }

        gun.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}