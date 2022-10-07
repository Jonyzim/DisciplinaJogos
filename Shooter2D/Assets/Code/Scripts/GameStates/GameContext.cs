using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{


    public GameStateIdle StateIdle = new GameStateIdle();
    public GameStateWave StateWave = new GameStateWave();
    public GameStatePaused StatePaused = new GameStatePaused();

    public bool canStartWave;

    public GameState CurGameState;

    public int CurWave = 0;

    public int PlayerCredit = 0;

    private void Start()
    {
        SwitchState(StateIdle);
    }

    private void Update()
    {
        CurGameState.Update(this);
    }

    public void SwitchState(GameState newState)
    {
        newState.Start(this);
        CurGameState = newState;
    }

}
