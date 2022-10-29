using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Misc
{
    public class GameEvents : MonoBehaviour
    {
        private static GameEvents _instance;

        [FormerlySerializedAs("IsMultiplayer")] public bool isMultiplayer;

        public static GameEvents Instance
        {
            get
            {
                if (_instance == null) Debug.LogError("GameEvents does not exist!");
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null)
                Destroy(gameObject);
            else
                _instance = this;
        }

        public event Action<int, Sprite, Sprite> OnPickWeapon;

        public void PickWeapon(int id, Sprite magazineSprite, Sprite backgroundSprite)
        {
            OnPickWeapon?.Invoke(id, magazineSprite, backgroundSprite);
        }

        public event Action<int, float> OnClipUpdate;

        public void MagazineUpdate(int id, float fillAmount)
        {
            OnClipUpdate?.Invoke(id, fillAmount);
        }

        public event Action<int, float> OnReloadUpdate;

        public void ReloadUpdate(int id, float fillAmount)
        {
            OnReloadUpdate?.Invoke(id, fillAmount);
        }

        public event Action OnWaveBegin;

        public void WaveBegin()
        {
            OnWaveBegin?.Invoke();
        }

        public event Action OnWaveEnd;

        public void WaveEnd()
        {
            OnWaveEnd?.Invoke();
        }

        public event Action<int> OnPause;

        public void Pause(int id)
        {
            OnPause?.Invoke(id);
        }

        public event Action<int, int> OnScoreUpdate;

        public void ScoreUpdate(int id, int addScore)
        {
            OnScoreUpdate?.Invoke(id, addScore);
        }

        public event Action<int, float> OnHealthUpdate;

        public void HealthUpdate(int id, float fillAmount)
        {
            OnHealthUpdate?.Invoke(id, fillAmount);
        }

        public event Action<int, bool> OnSetUiMode;

        public void SetUiMode(int id, bool mode)
        {
            OnSetUiMode?.Invoke(id, mode);
        }

        public event Action<int, uint, uint> OnAmmoUpdate;

        public void AmmoUpdate(int id, uint curAmmo, uint maxAmmo)
        {
            OnAmmoUpdate?.Invoke(id, curAmmo, maxAmmo);
        }
    }
}