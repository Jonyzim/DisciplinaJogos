using MWP.Buffs;
using MWP.Misc;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Interactables.Farm
{
    public class Plant : MonoBehaviour
    {
        [FormerlySerializedAs("_growthTime")] [SerializeField] private int growthTime;
        [FormerlySerializedAs("_plantSprites")] [SerializeField] private Sprite[] plantSprites;
        [FormerlySerializedAs("_groundSprites")] [SerializeField] private Sprite[] groundSprites;
        [FormerlySerializedAs("_plantRenderer")] [SerializeField] private SpriteRenderer plantRenderer;
        [FormerlySerializedAs("_groundRenderer")] [SerializeField] private SpriteRenderer groundRenderer;
        [FormerlySerializedAs("_buff")] [SerializeField] private Buff buff;
        private bool _isWatered;
        public int GrowthTime => growthTime;
        public int Growth { get; private set; }


        private void Start()
        {
            Growth = 1;
            plantRenderer.sprite = plantSprites[Growth - 1];
            GameEvents.Instance.OnWaveEnd += GrowPlant;
            _isWatered = false;
        }

        private void GrowPlant()
        {
            if (Growth < GrowthTime && _isWatered)
            {
                _isWatered = false;
                groundRenderer.sprite = groundSprites[0];
                Growth += 1;
                plantRenderer.sprite = plantSprites[Growth - 1];
            }
        }

        public void WaterPlant()
        {
            groundRenderer.sprite = groundSprites[1];
            _isWatered = true;
        }

        public void Use(Character.Character character)
        {
            character.AddBuff(Instantiate(buff));
            Destroy(gameObject);
        }
    }
}