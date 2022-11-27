using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Buffs
{
    public abstract class Buff : ScriptableObject
    {
        [FormerlySerializedAs("_uniqueId")] [Header("General")] [SerializeField] private int uniqueId;

        // Adicionar imagens(?)
        public Sprite buffIcon;

        [FormerlySerializedAs("_isInfinite")] [SerializeField] private bool isInfinite;

        [FormerlySerializedAs("_timer")] [SerializeField] [HideIf("isInfinite")]
        private float timer;

        public float Timer => timer;

        public float CurTimer { get; set; }

        [HideInInspector] public Character Owner;
        public int UniqueId => uniqueId;

        private void Awake()
        {
            CurTimer = timer;
        }

        public void UpdateBuff(float deltaTime)
        {
            if (isInfinite) return;


            CurTimer -= deltaTime;

            if (!(CurTimer <= 0)) return;
            
            // Debug.Log("Remove buff");
            Owner.RemoveBuff(this);
            Destroy(this);
        }


        public abstract void Grant();

        public abstract void Remove();
    }
}