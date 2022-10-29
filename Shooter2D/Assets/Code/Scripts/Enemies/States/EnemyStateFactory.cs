using System.Collections.Generic;

namespace MWP.Enemies.States
{
    public class EnemyStateFactory
    {
        private readonly Dictionary<string, EnemyState> _cache;

        private readonly Enemy _context;

        public EnemyStateFactory(Enemy context)
        {
            _context = context;

            _cache = new Dictionary<string, EnemyState>
            {
                { "SearchState", null },
                { "EngagedState", null }
            };
        }

        public EnemyState StateSearch
        {
            get { return _cache["SearchState"] ??= new EnemyStateSearch(_context, this); }
        }

        public EnemyState StateEngaged
        {
            get { return _cache["EngagedState"] ??= new EnemyStateEngaged(_context, this); }
        }
    }
}