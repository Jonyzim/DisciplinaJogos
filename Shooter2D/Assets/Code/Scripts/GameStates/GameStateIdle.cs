using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateIdle : GameState
{

    public override void Start(GameManager context)
    {
        // TODO: Settar fórmula de tempo entre waves
        context.WaveTimer = 10;
    }

    public override void Update(GameManager context)
    {
        context.WaveTimer -= Time.deltaTime;
        if (context.WaveTimer <= 0)
        {
            context.WaveTimer = 0;

            if (context.CanStartWave == 0)
            {
                context.SwitchState(context.StateWave);
            }
        }
    }

    public override void Pause(GameManager context)
    {
        context.SwitchState(context.StatePaused);
    }

}
