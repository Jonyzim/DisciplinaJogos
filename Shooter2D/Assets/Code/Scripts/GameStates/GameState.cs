using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected GameManager Context;
    protected GameStateFactory Factory;

    public abstract void StartState();
    public abstract void UpdateState();
    public abstract void ExitState();

    public GameState(GameManager context, GameStateFactory factory)
    {
        Context = context;
        Factory = factory;
    }

    public virtual void Pause()
    {
        Context.SwitchState(Factory.StatePaused);
    }

    protected void SwitchState(GameState newState)
    {
        Context.CurGameState.ExitState();
        newState.StartState();
        Context.CurGameState = newState;
    }
}
