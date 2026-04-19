using System;
using System.Collections.Generic;
using System.Linq;

namespace MTS.Core
{
    public class TowerStat
    {
        private float _baseValue;
        private List<Func<float, float>> _modifiers = new();

        public TowerStat(float baseValue)
        {
            _baseValue = baseValue;
        }

        public void AddModifier(Func<float, float> modifier)
        {
            _modifiers.Add(modifier);
        }

        public void RemoveModifier(Func<float, float> modifier)
        {
            _modifiers.Remove(modifier);
        }

        public float Value {
            get
            {
                // Apply all current modifiers to the base value and return
                return _modifiers.Aggregate(_baseValue, (current, modifier) => modifier(current));
            }
        }
    }
}