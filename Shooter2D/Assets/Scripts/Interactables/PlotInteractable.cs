using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotInteractable : Interactable
{
    Plant plant;
    public GameObject tempPrefab; //Substituir pela seleção futuramente
    [SerializeField] private SpriteRenderer defaultRenderer;

    protected override void Interact(int id)
    {
        Debug.Log("interacted");
        if (plant == null)
        {
            GameObject plantPrefab = tempPrefab;
            //Seleção de plantas entra aqui

            GameObject plantObject = Instantiate(plantPrefab, transform);
            SetPlant(plantObject.GetComponent<Plant>());
            defaultRenderer.enabled = false;
        }
        else
        {

            //Caso a planta não esteja madura
            if (plant.Growth < plant.GrowthTime)
            {
                plant.waterPlant();
            }

            else
            {
                plant.Use(id);
                defaultRenderer.enabled = true;
                plant = null;
            }
        }
    }

    private void SetPlant(Plant _plant)
    {
        plant = _plant;
    }
}
