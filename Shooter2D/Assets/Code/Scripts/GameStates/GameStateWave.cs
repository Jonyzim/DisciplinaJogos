using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateWave : GameState
{

    public override void Start(GameManager context)
    {
        GameEvents.Instance.WaveBegin();

        context.CurWave++;

        // TODO: Criar fórmula para quantidade de inimigos
        context.RemainingEnemies = 10;
    }

    public override void Update(GameManager context)
    {
        if (context.RemainingEnemies <= 0)
        {
            GameEvents.Instance.WaveEnd();
            context.SwitchState(context.StateIdle);
        }
    }

}
