using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public abstract class Buff : ScriptableObject
{
    [Header("General")]
    [SerializeField]
    private bool _canDuplicate;

    [SerializeField]
    private int uniqueId;

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
