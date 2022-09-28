using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents s_Instance;
    public bool IsMultiplayer;

    void Awake()
    {
        s_Instance = this;
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

    public event Action OnWaveChange;
    public void WaveChange()
    {
        if (OnWaveChange != null)
        {
            OnWaveChange();
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
}
