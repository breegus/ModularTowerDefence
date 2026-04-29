using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        /// <summary>
        /// Register new modifier
        /// </summary>
        public void AddModifier(Func<float, float> modifier)
        {
            if (!_modifiers.Contains(modifier))
                _modifiers.Add(modifier);
            else
                Debug.LogWarning("TowerStat: Tried to add modifier that already exists! Skipping to avoid errors...");
        }

        /// <summary>
        /// Remove a registered modifier
        /// </summary>
        public void RemoveModifier(Func<float, float> modifier)
        {
            if (_modifiers.Contains(modifier))
                _modifiers.Remove(modifier);
            else
                Debug.LogWarning("TowerStat: Tried to remove modifier that doesnt exist! Skipping to avoid errors...");
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