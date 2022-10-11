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

    protected override void Interact(int id)
    {
        Corpse.CurHealth = 20;
        Corpse.gameObject.SetActive(true);
    }



}
