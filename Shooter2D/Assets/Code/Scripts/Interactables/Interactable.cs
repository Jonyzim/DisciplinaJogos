using UnityEngine;

namespace MWP.Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        public static readonly int UseOutline = Shader.PropertyToID("_UseOutline");

        private int _triggerCount = 0;
        
        //Unity Methods
        protected virtual void Start()
        {
        }

        //?Talvez já esteja funcionando com o multiplayer
        private void OnTriggerEnter2D(Collider2D other)
        {
            var character = other.gameObject.GetComponent<Character>();
            if (character != null) character.AddInteractable(this);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var character = other.gameObject.GetComponent<Character>();
            if (character != null) character.RemoveInteractable(this);
        }

        public abstract void Interact(Character character);


        public virtual void Enter()
        {
            _triggerCount++;
            if (_triggerCount > 1)
            {
                return;
            }
        }

        public virtual void Exit()
        {
            _triggerCount--;
            if (_triggerCount > 0)
            {
                return;
            }
        }

        protected virtual void DestroyThis(Character character)
        {
            character.RemoveInteractable(this);
            Destroy(gameObject);
        }
    }
}