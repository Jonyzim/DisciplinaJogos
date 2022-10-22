using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateIdle : GameState
{

    public GameStateIdle(GameManager context, GameStateFactory factory) : base(context, factory) { }

    public override void StartState()
    {
        Context.WaveTimer = Context.WaveTime;
    }

    public override void UpdateState()
    {
        Context.WaveTimer -= Time.deltaTime;
        if (Context.WaveTimer <= 0)
        {
            Context.WaveTimer = 0;

            if (Context.CanStartWave == 0)
            {
                Context.SwitchState(Factory.StateWave);
            }
        }
    }


    public override void ExitState()
    {

    }

    public override void Pause()
    {
        Context.SwitchState(Factory.StatePaused);
    }

}
