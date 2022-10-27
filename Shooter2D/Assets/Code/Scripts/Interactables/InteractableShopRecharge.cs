using MWP.GameStates;
using UnityEngine;

namespace MWP.Interactables
{
    public class InteractableShopRecharge : Interactable
    {

        [SerializeField] private SpriteRenderer spriteRenderer;

        public override void Interact(Character character)
        {
            uint _priceMod = character.EquippedGun?.BulletCost ?? 0;
            float? percentage = character.EquippedGun?.GetAmmunitionPercentage();

            if (percentage == 1)
                return;

            int price = (int)(100 * (1f - (percentage ?? 1)));


            if (GameManager.Instance.TryBuy(price))
            {
                character.EquippedGun.RechargeAmmunition();
            }

        }

        public override void Enter()
        {
            spriteRenderer.material.SetInt("_UseOutline", 1);
        }

        public override void Exit()
        {
            spriteRenderer.material.SetInt("_UseOutline", 0);
        }

    }
}