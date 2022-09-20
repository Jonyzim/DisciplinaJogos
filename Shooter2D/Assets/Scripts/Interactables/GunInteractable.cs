using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteractable : Interactable
{
    [SerializeField] public GameObject gun;

    protected override void Interact(int id){
        Gun gunComponent = gun.GetComponent<Gun>();


        character[id-1].gun.drop(character[id-1]);

        gunComponent.pick(character[id-1]);


        Destroy(gameObject);
    }
}
