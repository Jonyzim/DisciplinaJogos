using System;
using NaughtyAttributes;
using UnityEngine;

namespace MWP.ScriptableObjects
{
    [Serializable]
    public struct GunEntry
    {
        [ShowAssetPreview] public GameObject GunPrefab;

        public int Price;
        public bool IsAvailable;
    }
}