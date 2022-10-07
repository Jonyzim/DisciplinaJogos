using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateWave : GameState
{
    public override void Start(GameContext context)
    {
        context.CurWave++;
    }

    public override void Update(GameContext context)
    {
        if (context.RemainingEnemies <= 0)
        {
            context.SwitchState(context.StateIdle);
        }
    }

}
