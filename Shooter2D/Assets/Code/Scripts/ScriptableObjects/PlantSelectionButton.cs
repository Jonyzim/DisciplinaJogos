using MWP.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MWP.ScriptableObjects
{
    public class PlantSelectionButton : Button, ISelectHandler, IPointerEnterHandler
    {
        public PlantEntry PlantInfo;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            Select();
        }

        public override void OnSelect(BaseEventData eventData)
        {
            // TODO: Fix reference later
            GetComponentInParent<PlantGridLayoutManager>().ChangePlantDisplay(gameObject, PlantInfo);
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            GetComponentInParent<PlantGridLayoutManager>().ChoosePlant(PlantInfo.PlantPrefab);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            GetComponentInParent<PlantGridLayoutManager>().ChoosePlant(PlantInfo.PlantPrefab);
        }
    }
}