using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteractable : Interactable
{
    [SerializeField] private Gun gunType;
    [SerializeField] private Gun currentMagazine;
    [SerializeField] private Gun currentBullets;
    protected override void Interact(){
        
    }
}
