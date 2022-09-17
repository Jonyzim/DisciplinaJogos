using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteractable : Interactable
{
    [SerializeField] public GameObject gun;

    protected override void Interact(int id){
        GameObject gunInstance = Instantiate(gun, character[id].gun.transform);
        Gun gunComponent = gunInstance.GetComponent<Gun>();

        character[id].gun.drop(character[id]);

        
        //Setta o parente como o personagem
        gunInstance.transform.parent = character[id].gameObject.transform;

        character[id].gun = gunComponent;

        Destroy(gameObject);
    }
}
