namespace MWP.GameStates
{
    public class GameStatePaused : GameState
    {
        private GameState _previousGameState;

        public GameStatePaused(GameManager context, GameStateFactory factory) : base(context, factory)
        {
        }

        public override void StartState()
        {
            _previousGameState = Context.CurGameState;
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
        }

        public override void Pause()
        {
            Context.SwitchState(_previousGameState);
        }
    }
}