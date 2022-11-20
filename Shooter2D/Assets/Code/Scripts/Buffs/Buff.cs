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

        [FormerlySerializedAs("_isInfinite")] [SerializeField] private bool isInfinite;

        [FormerlySerializedAs("_timer")] [SerializeField] [HideIf("isInfinite")]
        private float timer;

        [HideInInspector] public Character Owner;
        public int UniqueId => uniqueId;

        public void UpdateBuff(float deltaTime)
        {
            if (isInfinite) return;


            timer -= deltaTime;

            if (!(timer <= 0)) return;
            
            // Debug.Log("Remove buff");
            Owner.RemoveBuff(this);
            Destroy(this);
        }


        public abstract void Grant();

        public abstract void Remove();
    }
}