using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected List<Character> CharacterList = new List<Character>(4);

    protected abstract void Interact(int id);

    //Unity Methods
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            CharacterList.Add(null);
        }
    }

    //?Talvez já esteja funcionando com o multiplayer
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Character")
        {
            Character _character = other.gameObject.GetComponent<Character>();
            CharacterList.Insert(_character.CharacterId - 1, _character);
            CharacterList[_character.CharacterId - 1].onInteract += Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Character")
        {
            Character _character = other.gameObject.GetComponent<Character>();

            _character.onInteract -= Interact;
            CharacterList.RemoveAt(_character.CharacterId - 1);
        }
    }
}
