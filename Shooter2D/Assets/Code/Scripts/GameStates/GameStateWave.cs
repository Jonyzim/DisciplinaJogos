using MWP.Misc;

namespace MWP.GameStates
{
    public class GameStateWave : GameState
    {
        public GameStateWave(GameManager context, GameStateFactory factory) : base(context, factory)
        {
        }

        public override void StartState()
        {
            Context.CurWave++;

            Context.RemainingEnemies = GameManager.WaveMultiplier * Context.CurWave;

            GameEvents.Instance.WaveBegin();
        }

        public override void UpdateState()
        {
            if (Context.RemainingEnemies <= 0) Context.SwitchState(Factory.StateIdle);
        }

        public override void ExitState()
        {
            Context.PlayerCredit += (Context.CurWave * Context.creditWaveMultiplayer) + Context.creditBaseGain;
            GameEvents.Instance.WaveEnd();
        }
    }
}