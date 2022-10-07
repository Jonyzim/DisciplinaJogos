using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeAmmunitionInteractable : Interactable
{
    protected override void Interact(int id)
    {
        // TODO: Adicionar constante de preço para cada arma
        int price = (int)(100 * (1f - CharacterList[id].EquippedGun.GetAmmunitionPercentage()));

        if (GameManager.Instance.TryBuy(price))
        {
            CharacterList[id].EquippedGun.RechargeAmmunition();
        }
    }
}
