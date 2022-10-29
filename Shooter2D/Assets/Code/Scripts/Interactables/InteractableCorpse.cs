using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Interactables
{
    [RequireComponent(typeof(Character.Character))]
    public class InteractableCorpse : Interactable
    {
        [FormerlySerializedAs("Corpse")] public Character.Character corpse;
        [SerializeField] private SpriteRenderer spriteRenderer;
        

        public void Initialize(Character.Character character)
        {
            corpse = character;
            corpse.transform.parent = transform;
        }


        // TODO: Add execution time
        public override void Interact(Character.Character character)
        {
            corpse.gameObject.SetActive(true);
            corpse.transform.parent = null;
            corpse.CurHealth = 20;

            DestroyThis(character);
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