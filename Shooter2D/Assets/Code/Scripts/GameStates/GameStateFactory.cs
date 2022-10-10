using System.Collections.Generic;

public class GameStateFactory
{

    private GameManager _context;
    private Dictionary<string, GameState> _cache;

    public GameStateFactory(GameManager context)
    {
        _context = context;

        _cache = new Dictionary<string, GameState>
        {
            {"IdleState", null},
            {"WaveState", null},
            {"PausedState", null}
        };

    }

    public GameState StateIdle
    {
        get
        {
            return _cache["IdleState"] ??= new GameStateIdle(_context, this);
        }
    }

    public GameState StateWave
    {
        get
        {
            return _cache["WaveState"] ??= new GameStateWave(_context, this);
        }
    }

    public GameState StatePaused
    {
        get
        {
            return _cache["PausedState"] ??= new GameStatePaused(_context, this);
        }
    }
}
