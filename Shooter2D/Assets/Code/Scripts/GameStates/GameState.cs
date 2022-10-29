namespace MWP.GameStates
{
    public abstract class GameState
    {
        protected readonly GameManager Context;
        protected readonly GameStateFactory Factory;

        protected GameState(GameManager context, GameStateFactory factory)
        {
            Context = context;
            Factory = factory;
        }

        public abstract void StartState();
        public abstract void UpdateState();
        public abstract void ExitState();

        public virtual void Pause()
        {
            Context.SwitchState(Factory.StatePaused);
        }
    }
}