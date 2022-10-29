using NaughtyAttributes;
using UnityEngine;

namespace MWP.GameStates
{
    public class GameManager : MonoBehaviour
    {
        public const int WaveMultiplier = 60;

        private static GameManager _instance;

        public float WaveTime;

        [HideInInspector] public float WaveTimer;

        [HideInInspector] public int RemainingEnemies;

        [HideInInspector] public int CanStartWave;

        [HideInInspector] public int CurWave;

        public int PlayerCredit;

        private GameStateFactory _factory;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null) Debug.LogError("GameManager does not exist!");
                return _instance;
            }
        }

        public GameState CurGameState { get; set; }

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
            if (PlayerCredit >= price)
            {
                PlayerCredit -= price;
                return true;
            }
            // TODO: Implementar balão dizendo "Créditos insuficientes"

            return false;
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