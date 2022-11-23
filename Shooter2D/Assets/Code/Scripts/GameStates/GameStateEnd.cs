using UnityEngine;

namespace MWP.GameStates
{
    public class GameStateEnd : GameState
    {
        public GameStateEnd(GameManager context, GameStateFactory factory) : base(context, factory)
        {
        }

        public override void StartState()
        {
            var endScreen = Object.Instantiate(Context.EndScreen);
        }

        public override void UpdateState()
        {
        }


        public override void ExitState()
        {
        }
        
        public override void Pause()
        {
        }
    }

}