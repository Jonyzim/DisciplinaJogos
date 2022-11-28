using System;
using MWP.Buffs;
using UnityEngine;
using UnityEngine.UI;

namespace MWP.UI
{
    public class HUDBuffIconManager : MonoBehaviour
    {
        private Buff _buff;
        [SerializeField] private Image buffImage;
        [SerializeField] private Image radialTimer;

        private void Update()
        {
            radialTimer.fillAmount = _buff.CurTimer / _buff.Timer;
            if (_buff.CurTimer < 0)
            {
                Destroy(gameObject);
            }
        }

        public void Initialize(Buff buff)
        {
            _buff = buff;
            buffImage.sprite = buff.buffIcon;
        }
    }
}