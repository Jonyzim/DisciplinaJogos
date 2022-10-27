using System;
using UnityEngine;

namespace MWP.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlantListManager", menuName = "Shooter2D/PlantListManager", order = 0)]
    public class PlantListManager : ScriptableObject
    {
        public PlantEntry[] PlantList;





    }
}
