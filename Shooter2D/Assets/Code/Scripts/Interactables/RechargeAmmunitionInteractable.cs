using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeAmmunitionInteractable : Interactable
{
    protected override void Interact(int id)
    {
        // TODO: Implementar condição de créditos
        if (true)
        {
            // Chamar função de consumir os créditos
            CharacterList[id].EquippedGun.RechargeAmmunition();
        }
        // TODO: Implementar balão dizendo "Créditos insuficientes"
        else
        {

        }
    }
}
