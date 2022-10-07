using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (_instance != null)
            {
                Debug.LogError("GameManager does not exist!");
            }
            return _instance;

        }
    }
    private static GameManager _instance;


    public GameStateIdle StateIdle = new GameStateIdle();
    public GameStateWave StateWave = new GameStateWave();
    public GameStatePaused StatePaused = new GameStatePaused();

    public bool canStartWave;

    public GameState CurGameState;

    public int CurWave = 0;

    public int PlayerCredit = 0;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

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
