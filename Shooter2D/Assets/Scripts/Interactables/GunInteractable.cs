using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteractable : Interactable
{
    [SerializeField] public GameObject Gun;

    protected override void Interact(int id)
    {
        Gun gunComponent = Gun.GetComponent<Gun>();

        if (CharacterList[id - 1].EquippedGun != null)
        {
            CharacterList[id - 1].EquippedGun.Drop(CharacterList[id - 1]);
        }

        gunComponent.Pick(CharacterList[id - 1]);


        Destroy(gameObject);
    }
}
