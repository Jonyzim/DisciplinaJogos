using MWP.GameStates;
using MWP.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Interactables
{
    public class InteractableShopGun : InteractableGun
    {
        [FormerlySerializedAs("_gunListManager")] [SerializeField] private GunListManager gunListManager;

        [SerializeField] private TMP_Text priceText;

        [FormerlySerializedAs("_priceBox")] [SerializeField] private GameObject priceBox;
        [FormerlySerializedAs("_iconBox")] [SerializeField] private GameObject iconBox;

        private int _price;

        // Unity Methods
        protected override void Start()
        {
            base.Start();
            SpawnWeapon();
            priceBox.SetActive(false);
            iconBox.SetActive(true);
        }


        public override void Interact(Character.Character character)
        {
            if (GameManager.Instance.TryBuy(_price)) base.Interact(character);
        }

        public void SpawnWeapon()
        {
            GameObject randomGun;
            (randomGun, _price) = gunListManager.GetRandomWeapon();
            priceText.text = _price.ToString();

            if (base.newGun != null) Destroy(base.newGun.gameObject);
            base.newGun = Instantiate(randomGun, gameObject.transform);
        }

        public override void Enter()
        {
            base.Enter();
            priceBox.SetActive(true);
            iconBox.SetActive(false);
        }

        public override void Exit()
        {
            base.Exit();
            priceBox.SetActive(false);
            iconBox.SetActive(true);
        }
    }
}