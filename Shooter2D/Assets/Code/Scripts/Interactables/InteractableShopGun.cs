using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableShopGun : InteractableGun
{
    // TODO: Spawnar balão de texto com dinheiro necessário

    [SerializeField]
    private GunListManager _gunListManager;
    [SerializeField]
    private TMP_Text priceText;
    private int _price;

    protected override void Interact(int id)
    {
        if (GameManager.Instance.TryBuy(_price))
        {
            base.Interact(id);
        }
    }

    // Unity Methods
    protected override void Start()
    {
        base.Start();
        SpawnWeapon();
    }

    public void SpawnWeapon()
    {
        GameObject newGun;
        (newGun, _price) = _gunListManager.GetRandomWeapon();
        priceText.text = _price.ToString();

        if (NewGun != null)
        {
            Destroy(NewGun.gameObject);
        }
        NewGun = Instantiate(newGun, gameObject.transform);
    }
}
