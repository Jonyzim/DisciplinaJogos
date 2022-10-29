using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MWP.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "GunListManager", menuName = "Shooter2D/GunListManager", order = 0)]
    public class GunListManager : ScriptableObject
    {
        [SerializeField] private GunEntry[] GunList;

        public (GameObject, int) GetRandomWeapon()
        {
            int i;
            // Gerar números até gerar uma arma válida
            do
            {
                i = Random.Range(0, GunList.Length);
            } while (!GunList[i].IsAvailable);

            return (GunList[i].GunPrefab, GunList[i].Price);
        }

        public void UnlockGun(int index)
        {
            GunList[index].IsAvailable = true;
        }

        [Button]
        public void Save()
        {
            var saveDirectoryPath = Application.persistentDataPath + "/saveData/";
            var saveFilePath = saveDirectoryPath + "weapons.cow";
            var bf = new BinaryFormatter();

            if (!Directory.Exists(saveDirectoryPath)) Directory.CreateDirectory(saveDirectoryPath);
            var saveFile = File.Create(saveFilePath);

            var json = JsonUtility.ToJson(this);
            bf.Serialize(saveFile, json);

            saveFile.Close();
        }

        [Button]
        public void Load()
        {
            var saveDirectoryPath = Application.persistentDataPath + "/saveData/";
            var saveFilePath = saveDirectoryPath + "weapons.cow";
            var bf = new BinaryFormatter();

            if (File.Exists(saveFilePath))
            {
                var saveFile = File.Open(saveFilePath, FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(saveFile), this);
                saveFile.Close();
            }
        }
    }
}