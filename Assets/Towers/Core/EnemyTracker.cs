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

        public Enemy GetClosestTo(Vector3 pos)
        {
            Enemy closest = null;
            var minDist = float.MaxValue;

            foreach (var enemy in _enemies)
            {
                var dist = Vector3.Distance(pos, enemy.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = enemy;
                }
            }

            return closest;
        }

        public Enemy GetStrongest()
        {
            return _enemies.OrderByDescending(e => e.health).FirstOrDefault();
        }

        public Enemy GetWeakest()
        {
            return _enemies.OrderByDescending(e => e.health).LastOrDefault();
        }

        public Enemy GetRandom()
        {
            if (_enemies.Count == 0) return null;
            var index = Random.Range(0, _enemies.Count);
            return _enemies[index];
        }
    }
}