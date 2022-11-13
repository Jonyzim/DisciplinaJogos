using UnityEngine;

namespace MWP.Interactables
{
    public class InteractableMedicKit : Interactable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private int healAmount;
        
        public override void Interact(Character character)
        {
            character.UpdateHealth(healAmount);
            Destroy(gameObject);
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