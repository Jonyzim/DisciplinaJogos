using System.Collections.Generic;

public class EnemyStateFactory
{

    private Enemy _context;
    private Dictionary<string, EnemyState> _cache;

    public EnemyStateFactory(Enemy context)
    {
        _context = context;

        _cache = new Dictionary<string, EnemyState>
        {
            {"SearchState", null},
            {"EngagedState", null},
        };

    }

    public EnemyState StateSearch
    {
        get
        {
            return _cache["SearchState"] ??= new EnemyStateSearch(_context, this);
        }
    }

    public EnemyState StateEngaged
    {
        get
        {
            return _cache["EngagedState"] ??= new EnemyStateEngaged(_context, this);
        }
    }
}
