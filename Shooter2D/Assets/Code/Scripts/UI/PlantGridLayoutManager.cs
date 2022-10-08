using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantGridLayoutManager : MonoBehaviour
{
    public int OwnerId;

    [SerializeField] private TMP_Text _effectsText;
    [SerializeField] private GridLayoutGroup _plantGridLayout;
    [SerializeField] private PlantListManager _plantListManager;
    [SerializeField] private Sprite _unknownImage;

    public void Initialize(int id)
    {
        OwnerId = id;
        GameEvents.Instance.SetUiMode(OwnerId, true);

        // TODO: Por algum motivo ele spawna o dobro de gameObject vazios
        foreach (PlantEntry plant in _plantListManager.PlantList)
        {
            GameObject newPlantEntry = Instantiate(new GameObject(), _plantGridLayout.transform);

            // Selecionar a primeira planta na UI
            if (Player.s_ActivePlayers[OwnerId - 1].PlayerEventSystem.currentSelectedGameObject == null)
            {
                ChangePlantDisplay(plant);
                Player.s_ActivePlayers[OwnerId - 1].PlayerEventSystem.SetSelectedGameObject(newPlantEntry);
            }

            Image newImage = newPlantEntry.AddComponent<Image>();
            PlantSelectionButton newButton = newPlantEntry.AddComponent<PlantSelectionButton>();

            // Planta desbloqueada ou não
            if (plant.IsEnabled)
            {
                newImage.sprite = plant.PreviewImage;
                newButton.PlantInfo = plant;
            }
            else
            {
                newImage.sprite = _unknownImage;
            }
        }
    }


    public event Action<GameObject> OnChoosePlant;
    public void ChoosePlant(GameObject plantPrefab)
    {
        if (OnChoosePlant != null)
        {
            OnChoosePlant(plantPrefab);
        }
        GameEvents.Instance.SetUiMode(OwnerId, false);
        Destroy(gameObject);
    }

    public void ChangePlantDisplay(PlantEntry plantInfo)
    {
        _effectsText.text = plantInfo.EffectsDescription;
    }
}

public interface IBaseState
{
    public abstract void UpdateFunc();
}

public class State1 : IBaseState
{

    public void UpdateFunc()
    {

    }
}