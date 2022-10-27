using MWP.Misc;
using MWP.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MWP.UI
{
    public class PlantGridLayoutManager : MonoBehaviour
    {
        public int OwnerId;

        [SerializeField] private TMP_Text _effectsText;
        [SerializeField] private GridLayoutGroup _plantGridLayout;
        [SerializeField] private PlantListManager _plantListManager;
        [SerializeField] private Sprite _unknownImage;
        [SerializeField] private GameObject plantObject;

        public void Initialize(int id)
        {
            OwnerId = id;
            GameEvents.Instance.SetUiMode(OwnerId, true);

            // TODO: Por algum motivo ele spawna o dobro de gameObject vazios
            foreach (PlantEntry plant in _plantListManager.PlantList)
            {
                GameObject newPlantEntry = Instantiate(plantObject, _plantGridLayout.transform);


                Image newImage = newPlantEntry.GetComponent<Image>();
                PlantSelectionButton newButton = newPlantEntry.GetComponent<PlantSelectionButton>();

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

                // Selecionar a primeira planta na UI
                if (PlayerController.s_ActivePlayers[OwnerId - 1].PlayerEventSystem.currentSelectedGameObject == null)
                {
                    ChangePlantDisplay(newPlantEntry, plant);
                    PlayerController.s_ActivePlayers[OwnerId - 1].PlayerEventSystem.SetSelectedGameObject(newPlantEntry);
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
        private GameObject lastSelected = null;
        public void ChangePlantDisplay(GameObject buttonObj, PlantEntry plantInfo)
        {
            if (lastSelected != null)
                lastSelected.SetActive(false);
            lastSelected = buttonObj.transform.GetChild(0).gameObject;
            lastSelected.SetActive(true);
            _effectsText.text = plantInfo.EffectsDescription;
        }
    }
}
