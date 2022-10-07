using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateIdle : GameState
{
    private float _timer;

    public override void Start(GameContext context)
    {
        // TODO: Settar fórmula de tempo entre waves
        _timer = 10;
    }

    public override void Update(GameContext context)
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = 0;

            if (context.canStartWave)
            {
                context.SwitchState(context.StateWave);
            }
        }
    }

    public override void Pause(GameContext context)
    {
        context.SwitchState(context.StatePaused);
    }

}
