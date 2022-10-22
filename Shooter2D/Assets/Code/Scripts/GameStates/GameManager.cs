using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (GameManager._instance == null)
            {
                Debug.LogError("GameManager does not exist!");
            }
            return GameManager._instance;

        }
    }

    private static GameManager _instance;

    public float WaveTime;

    [HideInInspector]
    public float WaveTimer;

    [HideInInspector]
    public int RemainingEnemies;

    [HideInInspector]
    public int CanStartWave;

    public GameState CurGameState
    {
        get => _curGameState;
        set => _curGameState = value;
    }

    [HideInInspector]
    public int CurWave = 0;

    public int PlayerCredit = 0;

    private GameState _curGameState;

    private GameStateFactory _factory;

    private void Awake()
    {
        if (GameManager._instance != null)
        {
            Destroy(this);
            return;
        }

        GameManager._instance = this;
        _factory = new GameStateFactory(this);
    }

    private void Start()
    {
        SwitchState(_factory.StateIdle);
    }

    private void Update()
    {
        _curGameState.UpdateState();
    }

    public void SwitchState(GameState newState)
    {
        _curGameState?.ExitState();
        newState.StartState();
        _curGameState = newState;
    }

    public bool TryBuy(int price)
    {
        if (PlayerCredit >= price)
        {
            PlayerCredit -= price;
            return true;
        }
        // TODO: Implementar balão dizendo "Créditos insuficientes"
        else
        {
            return false;
        }
    }

#if (UNITY_EDITOR)
    [Button]
    private void FinishWave()
    {
        RemainingEnemies = 0;
    }

    [Button]
    private void StartWave()
    {
        WaveTimer = 0;
    }


#endif
}
