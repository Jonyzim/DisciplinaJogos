using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotInteractable : Interactable
{
    public GameObject TempPrefab; //Substituir pela seleção futuramente
    [SerializeField] private SpriteRenderer _defaultRenderer;
    private Plant _plant;

    protected override void Interact(int id)
    {
        if (_plant == null)
        {
            GameObject plantPrefab = TempPrefab;
            //Seleção de plantas entra aqui

            GameObject plantObject = Instantiate(plantPrefab, transform);
            SetPlant(plantObject.GetComponent<Plant>());
            _defaultRenderer.enabled = false;
        }
        else
        {

            //Caso a planta não esteja madura
            if (_plant.Growth < _plant.GrowthTime)
            {
                _plant.waterPlant();
            }

            else
            {
                _plant.Use(id);
                _defaultRenderer.enabled = true;
                _plant = null;
            }
        }
    }

    private void SetPlant(Plant plant)
    {
        _plant = plant;
    }
}
