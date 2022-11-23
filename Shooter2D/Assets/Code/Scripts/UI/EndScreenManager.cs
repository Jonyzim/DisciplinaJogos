using System;
using UnityEngine;
using TMPro;

namespace MWP.UI
{
    public class EndScreenManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] playerTexts;
        
        private void Start()
        {
            for (var i = 0; i < 4; i++)
            {
                var score = PlayerController.SActivePlayers[i]?.Score.ToString();
                if (score == null)
                {
                    playerTexts[i].enabled = false;
                    continue;
                }
                
                playerTexts[i].text = score;
                
            }
        }
    }
}