using MWP.ScriptableObjects;
using UnityEngine;

namespace MWP.Misc
{
    public class StartUp : MonoBehaviour
    {
        private static bool _initialized;
        [SerializeField] private GunListManager gunManager;
        [SerializeField] private PlantListManager plantManager;


        private void Awake()
        {
            if (_initialized) return;
            
            LoadValues();
            _initialized = true;
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            gunManager.Save();
            //plantManager.Save();
        }

        private void LoadValues()
        {
            gunManager.Load();
            //plantManager.Load();
        }
    }
}