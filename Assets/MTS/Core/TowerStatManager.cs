using System;
using System.Collections.Generic;
using UnityEngine;

namespace MTS.Core
{
    public class TowerStatManager
    {
        private Dictionary<string, TowerStat> _stats = new();

        /// <summary>
        /// Set the default value, useful for initializing modifiers that aren't being edited yet
        /// </summary>
        public void SetBase(string name, float value)
        {
            if (!_stats.ContainsKey(name))
                _stats[name] = new TowerStat(value);
            else
                Debug.LogWarning("TowerStatManager: Tried to set base value of an existing modifier! Skipping to avoid errors...");
        }

        /// <summary>
        /// Apply a modifier via name and how to modify it
        /// </summary>5
        public void AddModifier(string name, Func<float, float> modifier)
        {
            if (_stats.TryGetValue(name, out var stat))
                stat.AddModifier(modifier);
            else
                Debug.LogWarning("TowerStatManager: Could not find valid modifier... Has the base value been set?");
        }

        /// <summary>
        /// Remove a modifier via name
        /// </summary>
        public void RemoveModifier(string name, Func<float, float> modifier)
        {
            if (_stats.TryGetValue(name, out var stat))
                stat.RemoveModifier(modifier);
        }

        /// <summary>
        /// Fetch a modifier's value. If not found then 'defaultValue' will be returned.
        /// </summary>
        public float Get(string name, float defaultValue = 0f)
        {
            return _stats.TryGetValue(name, out var stat) ? stat.Value : defaultValue;
        }
    }
}
