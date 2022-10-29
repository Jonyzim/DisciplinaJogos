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

        [FormerlySerializedAs("_timer")] [SerializeField] [HideIf("_isInfinite")]
        private float timer;

        public int UniqueId => uniqueId;


        public static event Action<Buff> OnRemove;

        public void UpdateBuff(float deltaTime)
        {
            if (isInfinite) return;


            timer -= deltaTime;

            if (!(timer <= 0)) return;
            
            // Debug.Log("Remove buff");
            OnRemove?.Invoke(this);
            Destroy(this);
        }


        public abstract void Grant(Character.Character character);

        public abstract void Remove(Character.Character character);
    }
}