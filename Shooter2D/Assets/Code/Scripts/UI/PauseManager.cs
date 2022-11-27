using System;
using MWP.Misc;
using UnityEngine;

namespace MWP.UI
{
    public class PauseManager : MonoBehaviour
    {
        private static bool _sIsPaused;

        [SerializeField] private GameObject firstSelected;
        
        private PlayerController _ownerPlayer;

        public void Initialize(PlayerController player)
        {
            _ownerPlayer = player;
            
            _ownerPlayer.PlayerEventSystem.SetSelectedGameObject(firstSelected);
            GameEvents.Instance.SetUiMode(_ownerPlayer.PlayerId, true);
            PauseGame();
        }

        private void PauseGame()
        {
            if (_sIsPaused)
            {
                Destroy(gameObject);
                return;
            }
            _sIsPaused = true;
            Time.timeScale = 0f;
        }

        public void ClosePause()
        {
            _sIsPaused = false;
            Time.timeScale = 1f;
            
            GameEvents.Instance.SetUiMode(_ownerPlayer.PlayerId, false);
            
            Destroy(gameObject);
        }

    }
}