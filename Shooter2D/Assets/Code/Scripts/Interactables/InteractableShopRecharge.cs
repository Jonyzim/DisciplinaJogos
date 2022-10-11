using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableShopRecharge : Interactable
{

    protected override void Interact(int id)
    {
        uint _priceMod = CharacterList[id - 1].EquippedGun?.BulletCost ?? 0;

        int price = (int)(100 * (1f - (CharacterList[id - 1].EquippedGun?.GetAmmunitionPercentage() ?? 1)));

        if (GameManager.Instance.TryBuy(price))
        {
            CharacterList[id].EquippedGun.RechargeAmmunition();
        }
    }
}
