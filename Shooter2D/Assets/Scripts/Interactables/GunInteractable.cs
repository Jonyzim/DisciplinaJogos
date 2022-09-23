using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteractable : Interactable
{
    [SerializeField] public GameObject gun;

    protected override void Interact(int id)
    {
        Gun gunComponent = gun.GetComponent<Gun>();

        if (character[id - 1].gun != null)
        {
            character[id - 1].gun.Drop(character[id - 1]);
        }

        gunComponent.Pick(character[id - 1]);


        Destroy(gameObject);
    }
}
