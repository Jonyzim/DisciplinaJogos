using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameEvents does not exist!");
            }
            return _instance;

        }
    }
    public bool IsMultiplayer;

    private static GameEvents _instance;
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public event Action<int, Sprite, Sprite> OnPickWeapon;
    public void PickWeapon(int id, Sprite magazineSprite, Sprite backgroundSprite)
    {
        OnPickWeapon?.Invoke(id, magazineSprite, backgroundSprite);
    }

    public event Action<int, float> OnMagazineUpdate;
    public void MagazineUpdate(int id, float fillAmount)
    {
        OnMagazineUpdate?.Invoke(id, fillAmount);
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
}
