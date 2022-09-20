using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    void Awake()
    {
        current = this;
    }

    public event Action<int, Sprite, Sprite> onPickWeapon;
    public void PickWeapon(int id, Sprite magazineSprite, Sprite backgroundSprite){
        if(onPickWeapon != null)
            onPickWeapon(id, magazineSprite, backgroundSprite);
    }

    public event Action<int, float> onMagazineUpdate;
    public void MagazineUpdate(int id, float fillAmount){
        if(onMagazineUpdate != null){
            onMagazineUpdate(id, fillAmount);
        }
    }

    public event Action<int, float> onReloadUpdate;
    public void ReloadUpdate(int id, float fillAmount){
        if(onReloadUpdate != null){
            onReloadUpdate(id, fillAmount);
        }
    }

    public event Action onWaveChange;
    public void WaveChange(){
        if(onWaveChange != null){
            onWaveChange();
        }
    }

    public event Action<int, int> onScoreUpdate;
    public void ScoreUpdate(int id, int addScore){
        if(onScoreUpdate != null){
            onScoreUpdate(id, addScore);
        }
    }
}
