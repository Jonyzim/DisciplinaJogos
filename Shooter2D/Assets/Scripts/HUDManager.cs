using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public int id;
    public Image magazineSprite;
    public Image backgroundSprite;
    public Image reloadSprite;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPickWeapon += ChangeMagazine;
        GameEvents.current.onMagazineUpdate += UpdateMagazine;
        GameEvents.current.onReloadUpdate += UpdateReload;
    }

    private void ChangeMagazine(int _id, Sprite _magazineSprite, Sprite _backgroundSprite){
        if(id == _id)
        {
            magazineSprite.sprite = _magazineSprite;
            backgroundSprite.sprite = _backgroundSprite;
        }
    }

    private void UpdateMagazine(int _id, float fillAmount){
        if(id == _id)
            magazineSprite.fillAmount = fillAmount;
    }

    private void UpdateReload(int _id, float fillAmount){
        if(id == _id)
            reloadSprite.fillAmount = fillAmount;
    }
}
