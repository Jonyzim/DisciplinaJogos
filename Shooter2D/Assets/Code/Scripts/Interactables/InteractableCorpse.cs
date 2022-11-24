using System;
using MWP.GameStates;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MWP.Interactables
{
    public class InteractableCorpse : Interactable
    {
        [FormerlySerializedAs("Corpse")] public Character corpse;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private const float TimeToRevive = 3.0f;
        private float _curTimer;
        private bool _isInteracting;
        private Character _interactingCharacter;
        [SerializeField] private Image reviveWheelImage;
        

        public void Initialize(Character character)
        {
            corpse = character;
            corpse.transform.parent = transform;
            reviveWheelImage.fillAmount = 0;
        }


        // TODO: Add execution time
        public override void Interact(Character character)
        {
            if (!_isInteracting)
            {
                _isInteracting = true;
                character.DisableMovement();
                _curTimer = 0;
                _interactingCharacter = character;

            }
            
            else
            {
                _isInteracting = false;
                character.EnableMovement();
                _interactingCharacter = null;
                _curTimer = 0;
            }
        }

        private void Revive(Character character)
        {
            corpse.gameObject.SetActive(true);
            corpse.transform.parent = null;
            corpse.CurHealth = 20;
            GameManager.Instance.AddCharacter();

            DestroyThis(character);
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

        private void Update()
        {
            reviveWheelImage.fillAmount = _curTimer / TimeToRevive;
            
            if (!_isInteracting) return;
            
            _curTimer += Time.deltaTime;
            if (_curTimer >= TimeToRevive)
            { 
                _interactingCharacter.EnableMovement();
                Revive(_interactingCharacter);
            }
        }
    }
}