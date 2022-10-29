using MWP.Character;
using MWP.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Interactables.Farm
{
    public class InteractablePlot : Interactable
    {
        [FormerlySerializedAs("_defaultRenderer")] [SerializeField] private SpriteRenderer defaultRenderer;
        [FormerlySerializedAs("_plantSelectionPopup")] [SerializeField] private GameObject plantSelectionPopup;
        [SerializeField] private Transform canvas;
        private int _interactingId = -1;
        private bool _isLocked;

        private Plant _plant;

        // TODO: Quando no teclado não rega(?)
        public override void Interact(Character.Character character)
        {
            if (_plant == null)
            {
                if (_isLocked) return;
                //Seleção de plantas
                _isLocked = true;
                _interactingId = character.OwnerId;

                //PlantGridLayoutManager popupManager = Instantiate(_plantSelectionPopup, Player.s_ActivePlayers[id - 1].PlayerCanvas.transform).GetComponent<PlantGridLayoutManager>();

                var popupManager = Instantiate(plantSelectionPopup, canvas).GetComponent<PlantGridLayoutManager>();
                PlayerController.SActivePlayers[_interactingId - 1].PlayerEventSystem.playerRoot =
                    canvas.gameObject;
                popupManager.OnChoosePlant += SetPlant;
                popupManager.Initialize(_interactingId);
            }
            else
            {
                //Caso a planta não esteja madura
                if (_plant.Growth < _plant.GrowthTime)
                {
                    _plant.WaterPlant();
                }

                else
                {
                    _plant.Use(character);
                    defaultRenderer.enabled = true;
                    _plant = null;
                }
            }
        }

        private void SetPlant(GameObject plantPrefab)
        {
            if (plantPrefab != null)
            {
                var plantObject = Instantiate(plantPrefab, transform);
                _plant = plantObject.GetComponent<Plant>();

                defaultRenderer.enabled = false;
            }

            _isLocked = false;
            _interactingId = -1;
        }

        public override void Enter()
        {
            defaultRenderer.material.SetInt(UseOutline, 1);
        }

        public override void Exit()
        {
            defaultRenderer.material.SetInt(UseOutline, 0);
        }
    }
}