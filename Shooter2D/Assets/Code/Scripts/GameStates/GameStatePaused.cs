using System.Collections;
using System.Collections.Generic;

public class GameStatePaused : GameState
{
    private GameState _previousGameState;

    public override void Start(GameManager context)
    {
        _previousGameState = context.CurGameState;
    }

    public override void Update(GameManager context)
    {

    }

    public override void Pause(GameManager context)
    {
        context.SwitchState(_previousGameState);
    }

}
