using System;
using System.Collections.Generic;
using UnityEngine;

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

    private List<Buff> buffList = new List<Buff>();
    private int _curHealth;
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

        if (EquippedGun != null)
        {
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

    //Methods
    public void ChangeHealth(int value)
    {
        _curHealth -= value;
        GameEvents.s_Instance.HealthUpdate(_characterId, ((float)_curHealth / (float)Health));
        Debug.Log(_curHealth);

        if (_curHealth <= 0)
        {
            Debug.Log("Death");
            //Destroy(gameObject);
        }
    }

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

        if (EquippedGun != null)
        {
            EquippedGun.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void AddBuff(Buff buff)
    {
        buff.Grant(this);
        buffList.Add(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        buff.Remove(this);
        buffList.Remove(buff);
    }

    //Unity Methods
    void Start()
    {
        _curHealth = Health;
        _body = GetComponent<Rigidbody2D>();

        Buff.OnRemove += RemoveBuff;
    }

    void Update()
    {
        // Atualiza os timers dos buffs
        foreach (Buff buff in buffList)
        {
            buff.Update(Time.deltaTime);
        }
    }



}