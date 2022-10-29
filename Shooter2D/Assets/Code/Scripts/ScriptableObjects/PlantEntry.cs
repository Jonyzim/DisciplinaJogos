using System;
using UnityEngine;

namespace MWP.ScriptableObjects
{
    [Serializable]
    public struct PlantEntry
    {
        public string Name;

        public string Description;
        public string EffectsDescription;
        public Sprite PreviewImage;

        public GameObject PlantPrefab;
        public bool IsEnabled;
    }
}