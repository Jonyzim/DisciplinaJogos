using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateWave : GameState
{
    public int RemainingEnemies;

    public override void Start(GameManager context)
    {
        GameEvents.s_Instance.WaveBegin();

        context.CurWave++;

        // TODO: Criar fórmula para quantidade de inimigos
        RemainingEnemies = 10;
    }

    public override void Update(GameManager context)
    {
        if (RemainingEnemies <= 0)
        {
            GameEvents.s_Instance.WaveEnd();
            context.SwitchState(context.StateIdle);
        }
    }

}
