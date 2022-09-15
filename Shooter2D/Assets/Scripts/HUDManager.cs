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
        GameEvents.current.onPickWeapon += changeMagazine;
        GameEvents.current.onMagazineUpdate += updateMagazine;
        GameEvents.current.onReloadUpdate += updateReload;
    }

    private void changeMagazine(int _id, Sprite _magazineSprite, Sprite _backgroundSprite){
        Debug.Log("called");
        if(id == _id)
        {
            magazineSprite.sprite = _magazineSprite;
            backgroundSprite.sprite = _backgroundSprite;
        }
    }

    private void updateMagazine(int _id, float fillAmount){
        if(id == _id)
            magazineSprite.fillAmount = fillAmount;
    }

    private void updateReload(int _id, float fillAmount){
        if(id == _id)
            reloadSprite.fillAmount = fillAmount;
    }
}
