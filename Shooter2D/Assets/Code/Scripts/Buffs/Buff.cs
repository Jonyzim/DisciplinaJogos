using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Buff
{
    public static event Action<Buff> OnRemove;

    // Adicionar imagens(?)

    private bool _isInfinite;
    private float _timer;

    /// <summary>
    /// Infinite buff constructor
    /// </summary>
    public Buff()
    {
        _isInfinite = true;
    }

    public Buff(float duration)
    {
        _isInfinite = false;
        _timer = duration > 0 ? duration : 0;
    }

    public void Update(float deltaTime)
    {
        if (_isInfinite) return;

        _timer -= deltaTime;
        if (_timer <= 0)
        {
            if (OnRemove != null)
            {
                OnRemove(this);
            }
        }
    }


    public abstract void Grant(Character character);

    public abstract void Remove(Character character);
}
