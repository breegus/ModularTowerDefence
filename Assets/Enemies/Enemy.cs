using System;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public float health;

        public Enemy(float health)
        {
            this.health = health;
        }
        
        public void TakeDamage(float damage)
        {
            health -= damage;
            health = Math.Clamp(health, 0, 100);
            
            Debug.Log($"Ouch! I have {health} health left now.");
        }
    }
}
