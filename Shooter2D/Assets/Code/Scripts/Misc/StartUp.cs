using MWP.ScriptableObjects;
using UnityEngine;

namespace MWP.Misc
{
    public class StartUp : MonoBehaviour
    {
        [SerializeField] private GunListManager gunManager;
        [SerializeField] private PlantListManager plantManager;

        private static bool _initialized = false;


        private void Awake()
        {
            if (!_initialized)
            {
                LoadValues();

                _initialized = true;
                DontDestroyOnLoad(gameObject);
            }

        }

        private void LoadValues()
        {
            gunManager.Load();
            //plantManager.Load();
        }

        private void OnApplicationQuit()
        {

            gunManager.Save();
            //plantManager.Save();
        }

    }
}