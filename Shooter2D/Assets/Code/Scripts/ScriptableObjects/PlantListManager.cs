using System;
using MWP.Interactables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MWP.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlantListManager", menuName = "Shooter2D/PlantListManager", order = 0)]
    public class PlantListManager : ScriptableObject
    {
        public PlantEntry[] PlantList;

        public GameObject GetRandomPlant()
        {
            int i;
            // Gerar números até gerar uma planta válida
            do
            {
                i = Random.Range(0, PlantList.Length);
            } while (!PlantList[i].IsEnabled);

            return PlantList[i].PlantPrefab;
        }
    }
}