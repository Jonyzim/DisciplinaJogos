using System;
using MWP.Misc;
using MWP.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace MWP.Interactables
{
    [DisallowMultipleComponent]
    public class PlantRandomSpawner : Interactable
    {
        private const float SpawnChance = 0.2f;
        private Plant _plantInstance;
        private bool _hasPlant = false;
        [SerializeField] private PlantListManager plantListManager;

        public override void Interact(Character character)
        {
            if (!_hasPlant) return;
            if (!_plantInstance.IsFullyGrown) return;
            
            _plantInstance.Use(character);
            _hasPlant = false;
        }

        public override void Enter()
        {
            if (_plantInstance == null) return;
            if (_plantInstance.IsFullyGrown)
            {
                _plantInstance.Glow();
            }

        }

        public override void Exit()
        {
            if (_plantInstance == null) return;
            if (_plantInstance.IsFullyGrown)
            {
                _plantInstance.UnGlow();
            }
        }


        private void TrySpawnPlant()
        {
            // Prevents spawning 2 plants at the same time
            if (_hasPlant) return;
            
            // Chance of spawning every wave
            var i = Random.Range(0.0f, 1.0f);
            if (i > SpawnChance) return;
            
            // Creating Plant
            var plantPrefab = plantListManager.GetRandomPlant();
            _plantInstance = Instantiate(plantPrefab, transform.position, Quaternion.identity, transform).GetComponent<Plant>();
            _hasPlant = true;

        }

        private void WaterPlant()
        {
            if (_plantInstance == null) return;
            _plantInstance.WaterPlant();
        }
        
        protected override void Start()
        {
            base.Start();
            GameEvents.Instance.OnWaveBegin += WaterPlant;
            GameEvents.Instance.OnWaveEnd += TrySpawnPlant;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnWaveBegin -= WaterPlant;
            GameEvents.Instance.OnWaveEnd -= TrySpawnPlant;
        }
    }
}