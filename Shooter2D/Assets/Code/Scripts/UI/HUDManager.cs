using System;
using MWP.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using MWP.Buffs;

namespace MWP.UI
{
    public class HUDManager : MonoBehaviour
    {
        public int Id;
        public Image ClipSprite;
        public Image BackgroundSprite;
        public Image ReloadSprite;
        public TMP_Text ScoreText;
        public TMP_Text AmmoText;
        public int Score;
        [SerializeField] private Image IconImage;
        [SerializeField] private List<Sprite> characterIcons;
        [SerializeField] private Image HealthBarSprite;
        [SerializeField] private GameObject _pauseMenu;
        private bool _thisPlayerPaused;
        
        [Header("Buff List Variables")]
        [SerializeField] private GridLayoutGroup buffGrid;

        [SerializeField] private GameObject buffIconPrefab;
        
        public static bool s_IsPaused { get; private set; }
        


        //Unity Methods
        private void Start()
        {
            Score = 0;
            GameEvents.Instance.OnPickWeapon += ChangeClip;
            GameEvents.Instance.OnClipUpdate += UpdateClip;
            GameEvents.Instance.OnHealthUpdate += UpdateHealth;
            GameEvents.Instance.OnReloadUpdate += UpdateReload;
            GameEvents.Instance.OnAmmoUpdate += UpdateAmmo;
            GameEvents.Instance.OnScoreUpdate += AddScore;
            GameEvents.Instance.OnBuffUpdate += AddBuff;
        }

        //Methods
        public void SetupHUD(int id, string character)
        {
            Id = id;
            var rectTransform = GetComponent<RectTransform>();
            if (character=="Cow")
            {
                IconImage.sprite = characterIcons[0];
            }else if (character == "Pig")
            {
                IconImage.sprite = characterIcons[1];
            }else if (character == "Sheep")
            {
                IconImage.sprite = characterIcons[2];
            }else if (character == "Bull")
            {
                IconImage.sprite = characterIcons[3];

            }
            switch (Id)
            {
                case 1:
                    rectTransform.anchorMin = new Vector2(0, 1);
                    rectTransform.anchorMax = new Vector2(0, 1);
                    rectTransform.anchoredPosition = new Vector2(0, 0);
                    break;
                case 2:
                    rectTransform.anchorMin = new Vector2(1, 1);
                    rectTransform.anchorMax = new Vector2(1, 1);
                    rectTransform.anchoredPosition = new Vector2(-350, 0);
                    break;

                case 3:
                    rectTransform.anchorMin = new Vector2(0, 0);
                    rectTransform.anchorMax = new Vector2(0, 0);
                    rectTransform.anchoredPosition = new Vector2(0, 150);
                    break;
                case 4:
                    rectTransform.anchorMin = new Vector2(1, 0);
                    rectTransform.anchorMax = new Vector2(1, 0);
                    rectTransform.anchoredPosition = new Vector2(-350, 150);
                    break;
            }
        }

        private void ChangeClip(int id, Sprite magazineSprite, Sprite backgroundSprite)
        {
            if (Id == id)
            {
                ClipSprite.sprite = magazineSprite;
                BackgroundSprite.sprite = backgroundSprite;
            }
        }

        private void UpdateClip(int id, float fillAmount)
        {
            if (Id == id)
                ClipSprite.fillAmount = fillAmount;
        }

        private void UpdateHealth(int id, float fillAmount)
        {
            if (Id == id) HealthBarSprite.fillAmount = fillAmount;
        }

        private void UpdateReload(int id, float fillAmount)
        {
            if (Id == id)
                ReloadSprite.fillAmount = fillAmount;
        }

        private void UpdateAmmo(int id, uint curAmmo, uint maxAmmo)
        {
            if (Id == id)
            {
                string text;
                if (maxAmmo > 0)
                    text = $"{curAmmo} / {maxAmmo}";
                else
                    text = "";

                AmmoText.text = text;
            }
        }

        private void AddScore(int id, int n)
        {
            if (Id == id)
            {
                Score += n;
                ScoreText.text = Score.ToString().PadLeft(10, '0');
            }
        }

        private void AddBuff(int id, Buff buff)
        {
            var instance = Instantiate(buffIconPrefab, buffGrid.transform).GetComponent<HUDBuffIconManager>();
            instance.Initialize(buff);
        }


        private void OnDestroy()
        {
            GameEvents.Instance.OnPickWeapon -= ChangeClip;
            GameEvents.Instance.OnClipUpdate -= UpdateClip;
            GameEvents.Instance.OnHealthUpdate -= UpdateHealth;
            GameEvents.Instance.OnReloadUpdate -= UpdateReload;
            GameEvents.Instance.OnAmmoUpdate -= UpdateAmmo;
            GameEvents.Instance.OnScoreUpdate -= AddScore;
            GameEvents.Instance.OnBuffUpdate -= AddBuff;
        }
    }
}