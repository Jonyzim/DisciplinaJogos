using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Character"){
            Debug.Log("Enteredd");
            other.gameObject.GetComponent<Character>().onInteract += interact;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Character"){
            Debug.Log("Exited");
            other.gameObject.GetComponent<Character>().onInteract -= interact;
        }
    }

    void interact(){
        Debug.Log("INTERAGIU");
    }
}
