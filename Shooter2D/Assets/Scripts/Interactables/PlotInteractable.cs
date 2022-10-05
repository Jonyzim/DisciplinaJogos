using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotInteractable : Interactable
{
    [SerializeField] private SpriteRenderer _defaultRenderer;
    [SerializeField] private GameObject PlantSelectionPopup;
    private Plant _plant;
    private bool _isLocked = false;
    private int _interactingId = -1;

    protected override void Interact(int id)
    {
        if (_plant == null)
        {
            if (!_isLocked)
            {
                //Seleção de plantas
                _isLocked = true;
                _interactingId = id;
                GameObject popupInstance = Instantiate(PlantSelectionPopup, Player.s_ActivePlayers[id - 1].PlayerCanvas.transform);
                GameEvents.s_Instance.SetUiMode(id, true);
                popupInstance.GetComponent<PlantGridLayoutManager>().OnChoosePlant += SetPlant;
            }
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

    public void SetPlant(GameObject plantPrefab)
    {
        GameEvents.s_Instance.SetUiMode(_interactingId, false);
        if (plantPrefab != null)
        {
            GameObject plantObject = Instantiate(plantPrefab, transform);
            _plant = plantObject.GetComponent<Plant>();

            _defaultRenderer.enabled = false;
        }

        _isLocked = false;
        _interactingId = -1;


    }
}
