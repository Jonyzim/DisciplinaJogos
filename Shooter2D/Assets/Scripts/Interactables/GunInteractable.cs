using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteractable : Interactable
{
    [SerializeField] public GameObject gun;

    protected override void Interact(int id){
        Gun gunComponent = gun.GetComponent<Gun>();


        character[id].gun.drop(character[id]);

        gunComponent.pick(character[id]);


        Destroy(gameObject);
    }
}
