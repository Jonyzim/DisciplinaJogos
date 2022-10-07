using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGunInteractable : GunInteractable
{
    GunListManager gunListManager;
    private int _price;

    protected override void Interact(int id)
    {
        if (GameManager.Instance.TryBuy(_price))
        {
            base.Interact(id);
        }
    }

    // Unity Methods
    private void Start()
    {
        GameObject newGun;
        (newGun, _price) = gunListManager.GetRandomWeapon();

        NewGun = Instantiate(newGun, gameObject.transform);
    }
}
