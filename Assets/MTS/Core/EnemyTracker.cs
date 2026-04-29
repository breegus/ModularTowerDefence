using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;

namespace MTS.Core
{
    public class EnemyTracker : MonoBehaviour
    {
        private List<Enemy> _enemies = new();

        /// <summary>
        /// Add a new enemy to registered enemies
        /// </summary>
        /// <param name="enemy"></param>
        public void AddEnemy(Enemy enemy)
        {
            if (!_enemies.Contains(enemy))
                _enemies.Add(enemy);
            else
                Debug.LogWarning("EnemyTracker: Tried to add enemy that already exists! Skipping to avoid errors...");
        }

        /// <summary>
        /// Remove a registered enemy
        /// </summary>
        public void RemoveEnemy(Enemy enemy)
        {
            if (_enemies.Contains(enemy))
                _enemies.Remove(enemy);
            else
                Debug.LogWarning("EnemyTracker: Tried to remove enemy that does not exist! Skipping to avoid errors...");
        }

        /// <summary>
        /// Returns a copy of the list (Not original!)
        /// </summary>
        public List<Enemy> GetAll()
        {
            return new List<Enemy>(_enemies);
        }

        /// <summary>
        /// Finds the index of an enemy
        /// </summary>
        public int GetIndexOf(Enemy enemy)
        {
            return _enemies.IndexOf(enemy);
        }

        /// <summary>
        /// Return the enemy with the smallest given value.
        /// </summary>
        public Enemy GetMinBy<TKey>(Func<Enemy, TKey> selector)
        {
            if (_enemies.Count == 0)
                return null;

            var comparer = Comparer<TKey>.Default;
            var bestEnemy = _enemies[0];
            var bestValue = selector(bestEnemy);

            for (var i = 1; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                var candidateValue = selector(enemy);

                if (comparer.Compare(candidateValue, bestValue) >= 0) continue;

                bestEnemy = enemy;
                bestValue = candidateValue;
            }

            return bestEnemy;
        }

        /// <summary>
        /// Return the enemy with the largest given value.
        /// </summary>
        public Enemy GetMaxBy<TKey>(Func<Enemy, TKey> selector)
        {
            if (_enemies.Count == 0)
                return null;

            var comparer = Comparer<TKey>.Default;
            var bestEnemy = _enemies[0];
            var bestValue = selector(bestEnemy);

            for (var i = 1; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                var candidateValue = selector(enemy);

                if (comparer.Compare(candidateValue, bestValue) <= 0) continue;

                bestEnemy = enemy;
                bestValue = candidateValue;
            }

            return bestEnemy;
        }

        /// <summary>
        /// Return the enemy with the smallest projected value.
        /// </summary>
        public Enemy GetFirstAscending<TKey>(Func<Enemy, TKey> sortMethod)
        {
            return GetMinBy(sortMethod);
        }

        /// <summary>
        /// Return the enemy with the largest projected value.
        /// </summary>
        public Enemy GetFirstDescending<TKey>(Func<Enemy, TKey> sortMethod)
        {
            return GetMaxBy(sortMethod);
        }

        /// <summary>
        /// Get all enemies after sorting by ascending. Sorting method is provided
        /// </summary>
        public List<Enemy> GetAllAscending<TKey>(Func<Enemy, TKey> sortMethod)
        {
            return _enemies.OrderBy(sortMethod).ToList();
        }

        /// <summary>
        /// Get all enemies after sorting by descending. Sorting method is provided
        /// </summary>
        public List<Enemy> GetAllDescending<TKey>(Func<Enemy, TKey> sortMethod)
        {
            return _enemies.OrderByDescending(sortMethod).ToList();
        }
    }
}
