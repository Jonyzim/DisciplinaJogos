using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public GameStateIdle StateIdle = new GameStateIdle();
    public GameStateReady StateReady = new GameStateReady();
    public GameStateWave StateWave = new GameStateWave();

    private GameState _curGameState;

    public int CurWave;

    public int RemainingEnemies;

    public int PlayerCredit = 0;

    private void Update()
    {
        _curGameState.Update(this);
    }

    public void SwitchState(GameState newState)
    {
        _curGameState = newState;
        _curGameState.Start(this);
    }
}
