using System.Collections;
using System.Collections.Generic;

public class GameStatePaused : GameState
{
    public GameStatePaused(GameManager context, GameStateFactory factory) : base(context, factory) { }

    private GameState _previousGameState;

    public override void StartState()
    {
        _previousGameState = Context.CurGameState;
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }

    public override void Pause()
    {
        Context.SwitchState(_previousGameState);
    }

}
