using System.Collections.Generic;

namespace MWP.GameStates
{
    public class GameStateFactory
    {
        private readonly Dictionary<string, GameState> _cache;

        private readonly GameManager _context;

        public GameStateFactory(GameManager context)
        {
            _context = context;

            _cache = new Dictionary<string, GameState>
            {
                { "IdleState", null },
                { "WaveState", null },
                { "PausedState", null },
                { "EndState", null }
            };
        }

        public GameState StateIdle
        {
            get { return _cache["IdleState"] ??= new GameStateIdle(_context, this); }
        }

        public GameState StateWave
        {
            get { return _cache["WaveState"] ??= new GameStateWave(_context, this); }
        }

        public GameState StatePaused
        {
            get { return _cache["PausedState"] ??= new GameStatePaused(_context, this); }
        }

        public GameState StateEnd
        {
            get { return _cache["EndState"] ??= new GameStateEnd(_context, this); }
        }
    }
}