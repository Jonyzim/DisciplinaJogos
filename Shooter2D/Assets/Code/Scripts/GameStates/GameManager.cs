using MWP.Misc;
using NaughtyAttributes;
using UnityEngine;

namespace MWP.GameStates
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance
        {
            get
            {
                if (_instance == null) Debug.LogError("GameManager does not exist!");
                return _instance;
            }
        }

        public float HpMultiplier
        {
            get => _hpMultiplier;
            set
            {
                _hpMultiplier = value switch
                {
                    0 => 1.0f,
                    1 => 1.0f,
                    2 => 1.5f,
                    3 => 2.0f,
                    4 => 2.5f,
                    _ => _hpMultiplier
                };

                ;
            }
        }
        
        private float _hpMultiplier = 1.0f;
    
        public int WaveMultiplier = 2;

        private static GameManager _instance;

        public float WaveTime;

        [HideInInspector] public float WaveTimer;

        [HideInInspector] public int RemainingEnemies;

        [HideInInspector] public int CanStartWave;

        [HideInInspector] public int CurWave;

        public int PlayerCredit;

        private GameStateFactory _factory;

        public GameState CurGameState { get; private set; }
        
        public int creditBaseGain;

        public int creditWaveMultiplayer;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
            _factory = new GameStateFactory(this);
        }

        private void Start()
        {
            SwitchState(_factory.StateIdle);
        }

        private void Update()
        {
            CurGameState.UpdateState();
        }

        public void SwitchState(GameState newState)
        {
            CurGameState?.ExitState();
            newState.StartState();
            CurGameState = newState;
        }

        public bool TryBuy(int price)
        {
            if (PlayerCredit < price) return false;
            // TODO: Implementar balão dizendo "Créditos insuficientes"
            
            PlayerCredit -= price;
            return true;
            

        }

        private void AddCredit(int credit)
        {
            PlayerCredit += credit;
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
}