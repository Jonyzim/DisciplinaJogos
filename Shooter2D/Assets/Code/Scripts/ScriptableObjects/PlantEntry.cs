using System;
using NaughtyAttributes;
using UnityEngine;

namespace MWP.ScriptableObjects
{
    [Serializable]
    public struct PlantEntry
    {
        public string Name;

        public string Description;
        public string EffectsDescription;
        [ShowAssetPreview()]
        public Sprite PreviewImage;
        
        [ShowAssetPreview()]
        public GameObject PlantPrefab;
        public bool IsEnabled;
    }
}