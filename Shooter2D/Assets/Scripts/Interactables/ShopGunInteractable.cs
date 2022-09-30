using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGunInteractable : GunInteractable
{
    GunListManager gunListManager;

    protected override void Interact(int id)
    {
        // TODO: Implementar condição de créditos
        if (true)
        {
            // Chamar função de consumir os créditos
            base.Interact(id);
        }
        // TODO: Implementar balão dizendo "Créditos insuficientes"
        else
        {

        }
    }

    // Unity Methods
    private void Start()
    {
        NewGun = gunListManager.GetRandomWeapon();
    }
}
