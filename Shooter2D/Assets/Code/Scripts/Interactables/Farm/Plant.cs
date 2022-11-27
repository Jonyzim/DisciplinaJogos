using System;
using MWP.Buffs;
using MWP.Misc;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Interactables
{
    [DisallowMultipleComponent]
    public class Plant : MonoBehaviour
    {
        [FormerlySerializedAs("_growthTime")] [SerializeField] private int growthTime;
        [FormerlySerializedAs("_plantSprites")] [SerializeField] private Sprite[] plantSprites;
        [FormerlySerializedAs("_plantRenderer")] [SerializeField] private SpriteRenderer plantRenderer;
        [FormerlySerializedAs("_buff")] [SerializeField] private Buff buff;
        private bool _isWatered;
        public int GrowthTime => growthTime;
        public int Growth { get; private set; }

        public bool IsFullyGrown => Growth >= growthTime;


        private void Start()
        {
            Growth = 1;
            plantRenderer.sprite = plantSprites[Growth - 1];
            GameEvents.Instance.OnWaveEnd += GrowPlant;
            _isWatered = false;
        }

        private void GrowPlant()
        {
            if (Growth >= GrowthTime || !_isWatered) return;
            
            _isWatered = false;
            Growth += 1;
            plantRenderer.sprite = plantSprites[Growth - 1];
        }

        public void WaterPlant()
        {
            _isWatered = true;
        }

        public void Use(Character character)
        {
            Debug.Log("PLANT USED");
            character.AddBuff(Instantiate(buff));
            Destroy(gameObject);
        }

        public void Glow()
        {
            plantRenderer.material.SetInt(Interactable.UseOutline, 1);
        }
        
        public void UnGlow()
        {
            plantRenderer.material.SetInt(Interactable.UseOutline, 0);
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnWaveEnd -= GrowPlant;
        }
    }
}