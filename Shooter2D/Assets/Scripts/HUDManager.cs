using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public int id;
    public Image MagazineSprite;
    public Image BackgroundSprite;
    public Image ReloadSprite;
    public TMP_Text ScoreText;
    public int score;
    [SerializeField] private GameObject pauseMenu;
    static bool isPaused = false;
    static public bool IsPaused => isPaused;
    bool thisPlayerPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        GameEvents.current.onPickWeapon += ChangeMagazine;
        GameEvents.current.onMagazineUpdate += UpdateMagazine;
        GameEvents.current.onReloadUpdate += UpdateReload;
        GameEvents.current.onScoreUpdate += AddScore;
        GameEvents.current.onPause += PauseGame;
    }

    private void ChangeMagazine(int _id, Sprite _magazineSprite, Sprite _backgroundSprite){
        if(id == _id)
        {
            MagazineSprite.sprite = _magazineSprite;
            BackgroundSprite.sprite = _backgroundSprite;
        }
    }

    private void UpdateMagazine(int _id, float fillAmount){
        if(id == _id)
            MagazineSprite.fillAmount = fillAmount;
    }

    private void UpdateReload(int _id, float fillAmount){
        if(id == _id)
            ReloadSprite.fillAmount = fillAmount;
    }
    public void PauseGame(int _id)
    {
        if (id == _id)
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                isPaused = true;
                thisPlayerPaused = true;
            }
            else
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                isPaused = false;
                thisPlayerPaused = false;
            }
        }
    }
    public void AddScore(int _id, int n)
    {
        if(id == _id){
            score += n;
            ScoreText.text = score.ToString().PadLeft(10, '0');
        }
    }

    public void SetupHUD(int _id){
        id = _id;
        RectTransform rectTransform = this.GetComponent<RectTransform>();

        switch(id){
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
}
