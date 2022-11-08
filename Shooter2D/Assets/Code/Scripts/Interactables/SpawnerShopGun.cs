using System;
using MWP.Misc;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Interactables
{
    public class SpawnerShopGun : MonoBehaviour
    {
        public GameObject shopGunPrefab;
        private GameObject _gunInstance;
        private void Start()
        {
            GameEvents.Instance.OnWaveEnd += SpawnGun;
        }

        private void SpawnGun()
        {
            if (_gunInstance != null)
            {
                Destroy(_gunInstance);
            }
            _gunInstance = Instantiate(shopGunPrefab, transform);
            _gunInstance.transform.localPosition = Vector3.zero;
        }
    }
}