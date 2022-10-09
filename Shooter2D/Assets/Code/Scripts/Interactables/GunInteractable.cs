using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteractable : Interactable
{
    [SerializeField] public GameObject NewGun;

    protected override void Interact(int id)
    {
        Gun gunComponent = NewGun.GetComponent<Gun>();

        if (gunComponent != null)
        {
            CharacterList[id - 1].EquippedGun?.Drop(CharacterList[id - 1]);

            gunComponent.Pick(CharacterList[id - 1]);


            Destroy(gameObject);
        }
    }

}
