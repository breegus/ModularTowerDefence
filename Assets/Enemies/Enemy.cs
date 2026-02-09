using Towers.Core;
using System;
using UnityEngine;


namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public float health;
        private EnemyTracker _tracker;

        public Enemy(float health)
        {
            this.health = health;
        }

        public void OnEnable()
        {
            _tracker = FindFirstObjectByType<EnemyTracker>();
            _tracker.AddEnemy(this);
        }

        public void Die()
        {
            Debug.Log("Bleeeuugh, I have died!");
            
            _tracker.RemoveEnemy(this);
            Destroy(gameObject);
        }
        
        public void TakeDamage(float damage)
        {
            health -= damage;
            health = Math.Clamp(health, 0, 100);
            
            Debug.Log($"Ouch! I have {health} health left now.");

            if (health == 0)
            {
                Die();
            }
        }
    }
}
