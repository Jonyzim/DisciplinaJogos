using UnityEngine;

namespace MWP.Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        public static readonly int UseOutline = Shader.PropertyToID("_UseOutline");

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


        public abstract void Enter();
        public abstract void Exit();

        protected virtual void DestroyThis(Character character)
        {
            character.RemoveInteractable(this);
            Destroy(gameObject);
        }
    }
}