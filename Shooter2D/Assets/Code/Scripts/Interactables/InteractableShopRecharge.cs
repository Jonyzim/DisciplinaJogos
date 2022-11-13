using MWP.GameStates;
using UnityEngine;

namespace MWP.Interactables
{
    public class InteractableShopRecharge : Interactable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public override void Interact(Character character)
        {
            var priceMod = character.equippedGun?.bulletCost ?? 0;
            var percentage = character.equippedGun?.GetAmmunitionPercentage();

            if (percentage == 1f)
                return;

            var price = (int)(priceMod * (1f - (percentage ?? 1)));


            if (GameManager.Instance.TryBuy(price)) character.equippedGun.RechargeAmmunition();
        }

        public override void Enter()
        {
            spriteRenderer.material.SetInt(UseOutline, 1);
        }

        public override void Exit()
        {
            spriteRenderer.material.SetInt(UseOutline, 0);
        }
    }
}