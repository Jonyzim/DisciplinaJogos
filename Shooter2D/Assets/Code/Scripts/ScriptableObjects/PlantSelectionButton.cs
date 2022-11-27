using MWP.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MWP.ScriptableObjects
{
    public class PlantSelectionButton : Button
    {
        public PlantEntry PlantInfo;

        public override void OnSelect(BaseEventData eventData)
        {
            // TODO: Fix reference later
            SelectPlant();
        }

        private void SelectPlant()
        {
            GetComponentInParent<PlantGridLayoutManager>().ChangePlantDisplay(gameObject, PlantInfo);
        }

        public void ClickPlant()
        {
            GetComponentInParent<PlantGridLayoutManager>().ChoosePlant(PlantInfo.PlantPrefab);
        }
    }
}