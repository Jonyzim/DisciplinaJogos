using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableShopRecharge : Interactable
{

    protected override void Interact(int id)
    {
        uint _priceMod = CharacterList[id - 1].EquippedGun?.BulletCost ?? 0;
        float? percentage = CharacterList[id - 1].EquippedGun?.GetAmmunitionPercentage();
        Debug.Log(percentage);
        if (percentage == 1)
            return;

        int price = (int)(100 * (1f - (percentage ?? 1)));


        if (GameManager.Instance.TryBuy(price))
        {
            CharacterList[id - 1].EquippedGun.RechargeAmmunition();
        }
    }
}
