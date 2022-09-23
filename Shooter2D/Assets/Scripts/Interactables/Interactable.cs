using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected List<Character> character = new List<Character>(4);

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            character.Add(null);
        }
    }

    //?Talvez já esteja funcionando com o multiplayer
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Character")
        {
            Character _character = other.gameObject.GetComponent<Character>();
            character.Insert(_character.character_id - 1, _character);
            character[_character.character_id - 1].onInteract += Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Character")
        {
            Character _character = other.gameObject.GetComponent<Character>();

            _character.onInteract -= Interact;
            character.RemoveAt(_character.character_id - 1);
        }
    }
    protected abstract void Interact(int id);
}
