using UnityEngine;

namespace MWP.Interactables
{
    public class InteractableMedicKit : Interactable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private int healAmount;
        
        public override void Interact(Character character)
        {
            if (character.CurHealth == character.health) return;
            character.UpdateHealth(healAmount);
            Destroy(gameObject);

        }

        public override void Enter()
        {
            base.Enter();
            spriteRenderer.material.SetInt(UseOutline, 1);
        }

        public override void Exit()
        {
            base.Exit();
            spriteRenderer.material.SetInt(UseOutline, 0);
        }
    }
}