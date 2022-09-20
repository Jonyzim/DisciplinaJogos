using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private int growthTime;
    [SerializeField] private Sprite[] plantSprites;
    [SerializeField] private Sprite[] groundSprites;
    [SerializeField] private SpriteRenderer plantRenderer;
    [SerializeField] private SpriteRenderer groundRenderer;
    private bool isWatered;

    public int GrowthTime{
        get {return growthTime;}
    }

    private int growth;
    public int Growth{
        get {return growth;}
    }

    void Start(){
        growth = 1;
        plantRenderer.sprite = plantSprites[growth - 1];
        GameEvents.current.onWaveChange += GrowPlant;
        isWatered = false;
    }

    private void GrowPlant(){

        if(growth < GrowthTime && isWatered){
            isWatered = false;
            groundRenderer.sprite = groundSprites[0];
            growth += 1;
            plantRenderer.sprite = plantSprites[growth - 1];
        }
    }

    public void waterPlant(){
        groundRenderer.sprite = groundSprites[1];
        isWatered = true;
    }

    public void Use(int id){

        Destroy(gameObject);
    }
}
