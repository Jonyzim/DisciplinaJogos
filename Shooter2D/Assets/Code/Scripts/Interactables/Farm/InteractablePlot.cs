using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlot : Interactable
{
    [SerializeField] private SpriteRenderer _defaultRenderer;
    [SerializeField] private GameObject _plantSelectionPopup;
    [SerializeField] private Transform canvas;

    private Plant _plant;
    private bool _isLocked = false;
    private int _interactingId = -1;

    // TODO: Quando no teclado não rega(?)
    public override void Interact(Character character)
    {
        if (_plant == null)
        {
            if (!_isLocked)
            {
                //Seleção de plantas
                _isLocked = true;
                _interactingId = character.OwnerId;

                //PlantGridLayoutManager popupManager = Instantiate(_plantSelectionPopup, Player.s_ActivePlayers[id - 1].PlayerCanvas.transform).GetComponent<PlantGridLayoutManager>();

                PlantGridLayoutManager popupManager = Instantiate(_plantSelectionPopup, canvas).GetComponent<PlantGridLayoutManager>();
                Player.s_ActivePlayers[_interactingId - 1].PlayerEventSystem.playerRoot = canvas.gameObject;
                popupManager.OnChoosePlant += SetPlant;
                popupManager.Initialize(_interactingId);
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
                _plant.Use(character);
                _defaultRenderer.enabled = true;
                _plant = null;
            }
        }
    }

    public void SetPlant(GameObject plantPrefab)
    {
        if (plantPrefab != null)
        {
            GameObject plantObject = Instantiate(plantPrefab, transform);
            _plant = plantObject.GetComponent<Plant>();

            _defaultRenderer.enabled = false;
        }

        _isLocked = false;
        _interactingId = -1;

    }

    public override void Enter()
    {
        Debug.Log("ENTER");
        _defaultRenderer.material.SetInt("_UseOutline", 1);
    }

    public override void Exit()
    {
        Debug.Log("EXIT");
        _defaultRenderer.material.SetInt("_UseOutline", 0);
    }
}
