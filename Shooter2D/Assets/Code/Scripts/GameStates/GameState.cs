using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{

    public abstract void Start(GameManager context);
    public abstract void Update(GameManager context);

    public virtual void Pause(GameManager context)
    {
        context.SwitchState(context.StatePaused);
    }
}
