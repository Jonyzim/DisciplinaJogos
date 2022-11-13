using MWP.Misc;
using UnityEngine;

namespace MWP.Interactables
{
    public class SpawnerMedicKit : MonoBehaviour
    {
        public GameObject medicKitPrefab;
        private GameObject _medicKitInstance;
        private void Start()
        {
            GameEvents.Instance.OnWaveEnd += SpawnMedicKit;
        }

        private void SpawnMedicKit()
        {
            if (_medicKitInstance != null) return;
            _medicKitInstance = Instantiate(medicKitPrefab, transform);
            _medicKitInstance.transform.localPosition = Vector3.zero;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnWaveEnd -= SpawnMedicKit;
        }
    }
}