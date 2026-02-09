using System;
using System.Collections.Generic;

namespace Towers.Core
{
    public class TowerStats
    {
        private Dictionary<string, TowerStat> _stats = new();

        public void SetBase(string name, float value)
        {
            _stats[name] = new TowerStat(value);
        }

        public void AddModifer(string name, Func<float, float> modifier)
        {
            if (_stats.TryGetValue(name, out var stat))
                stat.AddModifier(modifier);
        }

        public void RemoveModifier(string name, Func<float, float> modifier)
        {
            if (_stats.TryGetValue(name, out var stat))
                stat.RemoveModifier(modifier);
        }

        public float Get(string name)
        {
            return _stats.TryGetValue(name, out var stat) ? stat.Value : 0f;
        }
    }
}
