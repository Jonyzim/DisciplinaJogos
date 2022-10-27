using NaughtyAttributes;
using System;
using UnityEngine;

namespace MWP.Buffs
{
    public abstract class Buff : ScriptableObject
    {
        public int UniqueId => _uniqueId;

        [Header("General")]
        [SerializeField]
        private int _uniqueId;

        // Adicionar imagens(?)

        [SerializeField]
        private bool _isInfinite;

        [SerializeField]
        [HideIf("_isInfinite")]
        private float _timer;


        public static event Action<Buff> OnRemove;

        public void UpdateBuff(float deltaTime)
        {
            if (_isInfinite) return;


            _timer -= deltaTime;

            if (_timer <= 0)
            {
                // Debug.Log("Remove buff");
                OnRemove?.Invoke(this);
                Destroy(this);
            }
        }


        public abstract void Grant(Character character);

        public abstract void Remove(Character character);
    }
}