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

            Context.RemainingEnemies = Context.WaveMultiplier * Context.CurWave;

            GameEvents.Instance.WaveBegin();
        }

        public override void UpdateState()
        {
            if (Context.RemainingEnemies <= 0) Context.SwitchState(Factory.StateIdle);
            if (Context.CharactersAlive <= 0) Context.SwitchState(Factory.StateEnd);
        }

        public override void ExitState()
        {
            Context.PlayerCredit += (Context.CurWave * Context.creditWaveMultiplayer) + Context.creditBaseGain;
            GameEvents.Instance.WaveEnd();
        }
    }
}