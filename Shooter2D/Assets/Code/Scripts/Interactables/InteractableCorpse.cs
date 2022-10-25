using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCorpse : Interactable
{
    public Character Corpse;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Initialize(Character character)
    {
        Corpse = character;
        Corpse.transform.parent = transform;
    }


    // TODO: Add execution time
    public override void Interact(Character character)
    {
        Corpse.gameObject.SetActive(true);
        Corpse.transform.parent = null;
        Corpse.CurHealth = 20;

        DestroyThis(character);
    }

    public override void Enter()
    {
        spriteRenderer.material.SetInt("_UseOutline", 1);
    }

    public override void Exit()
    {
        spriteRenderer.material.SetInt("_UseOutline", 0);
    }


}
