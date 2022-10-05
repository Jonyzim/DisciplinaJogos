using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantGridLayoutManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _plantGridLayout;
    [SerializeField] private PlantListManager _plantListManager;
    [SerializeField] private Sprite _unknownImage;

    private void Start()
    {
        foreach (PlantEntry plant in _plantListManager.PlantList)
        {
            GameObject newPlantEntry = Instantiate(new GameObject(), _plantGridLayout.transform);
            Image newImage = newPlantEntry.AddComponent<Image>();
            Button newButton = newPlantEntry.AddComponent<Button>();

            if (plant.IsEnabled)
            {
                newImage.sprite = plant.PreviewImage;
                newButton.onClick.AddListener(delegate { ChoosePlant(plant.PlantPrefab); });

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
        Destroy(gameObject);
    }

}
