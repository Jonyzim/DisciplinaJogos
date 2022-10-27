using MWP.GameStates;
using MWP.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace MWP.Interactables
{
    public class InteractableShopGun : InteractableGun
    {
        [SerializeField]
        private GunListManager _gunListManager;

        [SerializeField]
        private TMP_Text priceText;

        private int _price;

        [SerializeField] private GameObject _priceBox;
        [SerializeField] private GameObject _iconBox;


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
            _priceBox.SetActive(false);
            _iconBox.SetActive(true);
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
            _priceBox.SetActive(true);
            _iconBox.SetActive(false);
        }

        public override void Exit()
        {
            base.Exit();
            _priceBox.SetActive(false);
            _iconBox.SetActive(true);
        }


    }
}