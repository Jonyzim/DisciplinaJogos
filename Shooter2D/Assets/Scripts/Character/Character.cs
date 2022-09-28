using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe futuramente abstrata, serão utilizados as classes CowCharacter, PigCharacter e etc

public class Character : MonoBehaviour
{
    public Gun EquippedGun;

    [Header("Status")]
    [Range(80, 120)] public int Health;
    [Range(80, 120)] public int Strenght;
    [Range(80, 120)] public int Aim;
    [Range(80, 120)] public int Speed;
    public int CharacterId => _characterId;
    private int _characterId;

    private Rigidbody2D _body;
    private float _baseSpeed = 10;

    //Methods
    public void SetPlayerControlling(Player p)
    {
        if (p != null)
        {
            _characterId = p.PlayerId;
        }
        else
            _characterId = 0;
        if(EquippedGun != null){
            EquippedGun.SetOwner(this);
        }
    }


    //Custom Events
    public event Action<int> onInteract;
    public void Interact(int id)
    {
        if (onInteract != null)
        {
            onInteract(id);
        }
    }
    public void GetDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            print("Death");
            //Destroy(gameObject);
        }
    }
    //Methods
    public void Fire(Vector2 direction)
    {
        EquippedGun.Fire(direction, Strenght, Aim);
    }

    public void ReleaseFire()
    {
        EquippedGun.ReleaseFire();
    }


    public void Reload()
    {
        EquippedGun.Reload();
    }

    public void SwitchLight()
    {
        EquippedGun.SwitchFlashlight();
    }

    public void Move(Vector2 velocity)
    {
        _body.velocity = velocity * _baseSpeed * ((float)Speed / 100);
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

        if(EquippedGun != null){
            EquippedGun.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    //Unity Methods
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }


}