using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int GrowthTime => _growthTime;
    public int Growth => _growth;

    [SerializeField] private int _growthTime;
    [SerializeField] private Sprite[] _plantSprites;
    [SerializeField] private Sprite[] _groundSprites;
    [SerializeField] private SpriteRenderer _plantRenderer;
    [SerializeField] private SpriteRenderer _groundRenderer;
    private bool _isWatered;
    private int _growth;


    void Start()
    {
        _growth = 1;
        _plantRenderer.sprite = _plantSprites[_growth - 1];
        GameEvents.s_Instance.OnWaveEnd += GrowPlant;
        _isWatered = false;
    }

    private void GrowPlant()
    {

        if (_growth < GrowthTime && _isWatered)
        {
            _isWatered = false;
            _groundRenderer.sprite = _groundSprites[0];
            _growth += 1;
            _plantRenderer.sprite = _plantSprites[_growth - 1];
        }
    }

    public void waterPlant()
    {
        _groundRenderer.sprite = _groundSprites[1];
        _isWatered = true;
    }

    public void Use(int id)
    {

        Destroy(gameObject);
    }
}
