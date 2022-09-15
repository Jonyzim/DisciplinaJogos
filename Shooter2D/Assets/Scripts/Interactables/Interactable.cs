using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    List<Character> character = new List<Character>();

    //!Não suporta multiplayer ainda, tem que diferenciar o gameObject caso vários players estejam dentro do trigger
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
    protected virtual void Interact(){
        Debug.Log("INTERAGIU");
    }
}
