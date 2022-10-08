using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public int Id;
    public Image MagazineSprite;
    public Image BackgroundSprite;
    public Image ReloadSprite;
    public TMP_Text ScoreText;
    public int Score;
    public static bool s_IsPaused => isPaused;

    [SerializeField] private Image HealthBarSprite;
    [SerializeField] private GameObject _pauseMenu;
    private static bool isPaused = false;
    private bool _thisPlayerPaused = false;

    //Methods
    public void SetupHUD(int id)
    {
        Id = id;
        RectTransform rectTransform = this.GetComponent<RectTransform>();

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

    private void ChangeMagazine(int id, Sprite magazineSprite, Sprite backgroundSprite)
    {
        if (Id == id)
        {
            MagazineSprite.sprite = magazineSprite;
            BackgroundSprite.sprite = backgroundSprite;
        }
    }

    private void UpdateMagazine(int id, float fillAmount)
    {
        if (Id == id)
            MagazineSprite.fillAmount = fillAmount;
    }

    private void UpdateHealth(int id, float fillAmount)
    {
        if (Id == id)
            HealthBarSprite.fillAmount = fillAmount;
    }

    private void UpdateReload(int id, float fillAmount)
    {
        if (Id == id)
            ReloadSprite.fillAmount = fillAmount;
    }
    private void PauseGame(int id)
    {
        if (Id == id)
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                _pauseMenu.SetActive(true);
                isPaused = true;
                _thisPlayerPaused = true;
            }
            else
            {
                Time.timeScale = 1f;
                _pauseMenu.SetActive(false);
                isPaused = false;
                _thisPlayerPaused = false;
            }
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


    //Unity Methods
    void Start()
    {
        Score = 0;
        GameEvents.Instance.OnPickWeapon += ChangeMagazine;
        GameEvents.Instance.OnMagazineUpdate += UpdateMagazine;
        GameEvents.Instance.OnHealthUpdate += UpdateHealth;
        GameEvents.Instance.OnReloadUpdate += UpdateReload;
        GameEvents.Instance.OnScoreUpdate += AddScore;
        GameEvents.Instance.OnPause += PauseGame;
    }
}
