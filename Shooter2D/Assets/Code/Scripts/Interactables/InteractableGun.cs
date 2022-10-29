using MWP.Guns;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Interactables
{
    public class InteractableGun : Interactable
    {
        [FormerlySerializedAs("NewGun")] [SerializeField] public GameObject newGun;

        public override void Interact(Character character)
        {
            var gunComponent = newGun.GetComponent<Gun>();

            if (gunComponent == null) return;
            
            character.equippedGun?.Drop(character);
            gunComponent.Pick(character);
            DestroyThis(character);
        }

        public override void Enter()
        {
            newGun.GetComponent<SpriteRenderer>().material.SetInt(UseOutline, 1);
        }

        public override void Exit()
        {
            newGun.GetComponent<SpriteRenderer>().material.SetInt(UseOutline, 0);
        }
    }
}