using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected List<Character> character = new List<Character>();

    //?Talvez já esteja funcionando com o multiplayer
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Character"){
            Character _character = other.gameObject.GetComponent<Character>();

            character.Insert(_character.character_id, _character);
            character[character.Count-1].onInteract += Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Character"){
            Character _character = other.gameObject.GetComponent<Character>();

            _character.onInteract -= Interact;
            character.RemoveAt(_character.character_id);
        }
    }
    protected virtual void Interact(int id){
        //Debug.Log("INTERAGIU");
    }
}
