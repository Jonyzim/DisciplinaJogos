using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGun : Interactable
{
    [SerializeField] public GameObject NewGun;

    public override void Interact(Character character)
    {
        Gun gunComponent = NewGun.GetComponent<Gun>();

        if (gunComponent != null)
        {
            character.EquippedGun?.Drop(character);

            gunComponent.Pick(character);


            DestroyThis(character);
        }
    }

    public override void Enter()
    {
        NewGun.GetComponent<SpriteRenderer>().material.SetInt("_UseOutline", 1);
    }

    public override void Exit()
    {
        NewGun.GetComponent<SpriteRenderer>().material.SetInt("_UseOutline", 0);
    }
}
