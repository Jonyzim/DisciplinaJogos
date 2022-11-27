using System;
using MWP.Misc;
using TMPro;
using UnityEngine;

namespace MWP.UI
{
    public class PauseManager : MonoBehaviour
    {
        private static bool _sIsPaused;

        [SerializeField] private TMP_Text titleText;

        [SerializeField] private GameObject firstSelected;
        
        private PlayerController _ownerPlayer;

        public void Initialize(PlayerController player)
        {
            if (_sIsPaused)
            {
                Destroy(gameObject);
                return;
            }

            titleText.text = $"Player {player.PlayerId} paused";
            
            _ownerPlayer = player;
            
            _ownerPlayer.PlayerEventSystem.SetSelectedGameObject(firstSelected);
            GameEvents.Instance.SetUiMode(_ownerPlayer.PlayerId, true);
            PauseGame();
        }

        private void PauseGame()
        {
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