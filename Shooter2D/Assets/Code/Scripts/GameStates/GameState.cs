using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{

    public abstract void Start(GameContext context);
    public abstract void Update(GameContext context);

    public virtual void Pause(GameContext context)
    {
        context.SwitchState(context.StatePaused);
    }
}
