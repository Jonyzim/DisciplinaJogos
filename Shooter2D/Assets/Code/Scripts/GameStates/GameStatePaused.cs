using System.Collections;
using System.Collections.Generic;

public class GameStatePaused : GameState
{
    private GameState _previousGameState;

    public override void Start(GameContext context)
    {
        _previousGameState = context.CurGameState;
    }

    public override void Update(GameContext context)
    {

    }

    public override void Pause(GameContext context)
    {
        context.SwitchState(_previousGameState);
    }

}
