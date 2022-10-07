using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "PlantListManager", menuName = "Shooter2D/PlantListManager", order = 0)]
public class PlantListManager : ScriptableObject
{
    public PlantEntry[] PlantList;





}

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