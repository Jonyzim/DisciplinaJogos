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

    [SerializeField] private GameObject _textBox;

    public override void Interact(Character character)
    {
        if (GameManager.Instance.TryBuy(_price))
        {
            base.Interact(character);
        }
    }

    // Unity Methods
    protected override void Start()
    {
        base.Start();
        SpawnWeapon();
        _textBox.SetActive(false);
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

    public override void Enter()
    {
        base.Enter();
        _textBox.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        _textBox.SetActive(false);
    }


}
