using UnityEngine;

namespace MWP.Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        private int _flagEnter;
        private int _flagExit;

        public abstract void Interact(Character character);

        //Unity Methods
        protected virtual void Start()
        {
        }

        //?Talvez já esteja funcionando com o multiplayer
        private void OnTriggerEnter2D(Collider2D other)
        {

            Character _character = other.gameObject.GetComponent<Character>();
            if (_character != null)
            {
                _character.AddInteractable(this);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Character _character = other.gameObject.GetComponent<Character>();
            if (_character != null)
            {
                _character.RemoveInteractable(this);
            }
        }


        public abstract void Enter();
        public abstract void Exit();

        protected virtual void DestroyThis(Character character)
        {
            character.RemoveInteractable(this);
            Destroy(gameObject);

        }
    }
}