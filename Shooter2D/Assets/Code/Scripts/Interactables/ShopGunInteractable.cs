using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGunInteractable : GunInteractable
{
    // TODO: Spawnar balão de texto com dinheiro necessário

    [SerializeField]
    private GunListManager _gunListManager;

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
        SpawnWeapon();
    }

    public void SpawnWeapon()
    {
        GameObject newGun;
        (newGun, _price) = _gunListManager.GetRandomWeapon();


        if (NewGun != null)
        {
            Destroy(NewGun.gameObject);
        }
        NewGun = Instantiate(newGun, gameObject.transform);
    }
}
