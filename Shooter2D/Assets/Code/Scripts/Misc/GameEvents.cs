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
        if (OnPickWeapon != null)
            OnPickWeapon(id, magazineSprite, backgroundSprite);
    }

    public event Action<int, float> OnMagazineUpdate;
    public void MagazineUpdate(int id, float fillAmount)
    {
        if (OnMagazineUpdate != null)
        {
            OnMagazineUpdate(id, fillAmount);
        }
    }

    public event Action<int, float> OnReloadUpdate;
    public void ReloadUpdate(int id, float fillAmount)
    {
        if (OnReloadUpdate != null)
        {
            OnReloadUpdate(id, fillAmount);
        }
    }

    public event Action OnWaveBegin;
    public void WaveBegin()
    {
        if (OnWaveBegin != null)
        {
            OnWaveBegin();
        }
    }

    public event Action OnWaveEnd;
    public void WaveEnd()
    {
        if (OnWaveEnd != null)
        {
            OnWaveEnd();
        }
    }

    public event Action<int> OnPause;
    public void Pause(int id)
    {
        if (OnPause != null)
        {
            OnPause(id);
        }
    }

    public event Action<int, int> OnScoreUpdate;
    public void ScoreUpdate(int id, int addScore)
    {
        if (OnScoreUpdate != null)
        {
            OnScoreUpdate(id, addScore);
        }
    }

    public event Action<int, float> OnHealthUpdate;
    public void HealthUpdate(int id, float fillAmount)
    {
        if (OnHealthUpdate != null)
        {
            OnHealthUpdate(id, fillAmount);
        }
    }

    public event Action<int, bool> OnSetUiMode;
    public void SetUiMode(int id, bool mode)
    {
        if (OnSetUiMode != null)
        {
            OnSetUiMode(id, mode);
        }
    }
}
