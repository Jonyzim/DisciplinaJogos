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
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        GameEvents.current.onPickWeapon += ChangeMagazine;
        GameEvents.current.onMagazineUpdate += UpdateMagazine;
        GameEvents.current.onReloadUpdate += UpdateReload;
        GameEvents.current.onScoreUpdate += AddScore;
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

    public void AddScore(int _id, int n)
    {
        if(id == _id){
            score += n;
            ScoreText.text = score.ToString().PadLeft(10, '0');
        }
    }
}
