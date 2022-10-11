using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCorpse : Interactable
{
    public Character Corpse;

    public void Initialize(Character character)
    {
        Corpse = character;
        Corpse.transform.parent = transform;
    }


    // TODO: Add execution time
    protected override void Interact(int id)
    {
        Corpse.gameObject.SetActive(true);
        Corpse.transform.parent = null;
        Corpse.CurHealth = 20;

        Destroy(gameObject);
    }



}
