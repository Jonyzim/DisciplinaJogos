namespace MWP.Enemies.States
{
    public abstract class EnemyState
    {
        protected const float _astarTimer = 0.21f;
        protected Enemy Context;
        protected EnemyStateFactory Factory;

        public abstract void StartState();
        public abstract void UpdateState();
        public abstract void ExitState();

        public EnemyState(Enemy context, EnemyStateFactory factory)
        {
            Context = context;
            Factory = factory;
        }
    }
}