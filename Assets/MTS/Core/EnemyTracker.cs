using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;

namespace Towers.Core
{
    public class EnemyTracker : MonoBehaviour
    {
        private List<Enemy> _enemies = new();

        public void AddEnemy(Enemy enemy)
        {
            Debug.Log("Added enemy");
            if (!_enemies.Contains(enemy))
                _enemies.Add(enemy);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
        }

        public List<Enemy> GetAll()
        {
            return new List<Enemy>(_enemies);  // Return copy of list, not original!
        }

        public int GetIndexOf(Enemy enemy)
        {
            return _enemies.IndexOf(enemy);
        }

        public Enemy GetFirstAscending<TKey>(Func<Enemy, TKey> sortMethod)
        {
            return _enemies.OrderBy(sortMethod).FirstOrDefault();
        }

        public Enemy GetFirstDescending<TKey>(Func<Enemy, TKey> sortMethod)
        {
            return _enemies.OrderByDescending(sortMethod).FirstOrDefault();
        }

        public List<Enemy> GetAllAscending<TKey>(Func<Enemy, TKey> sortMethod)
        {
            return _enemies.OrderBy(sortMethod).ToList();
        }

        public List<Enemy> GetAllDescending<TKey>(Func<Enemy, TKey> sortMethod)
        {
            return _enemies.OrderByDescending(sortMethod).ToList();
        }
    }
}