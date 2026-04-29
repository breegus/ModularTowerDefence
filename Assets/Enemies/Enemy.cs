using System;
using System.Globalization;
using MTS.Core;
using TMPro;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public float health;
        public TMP_Text healthText;
        private EnemyTracker _tracker;

        public Enemy(float health)
        {
            this.health = health;
            
        }

        public void OnEnable()
        {
            _tracker = FindFirstObjectByType<EnemyTracker>();
            _tracker.AddEnemy(this);
            if (healthText)
                healthText.text = health.ToString(CultureInfo.InvariantCulture);
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
            if (health < 0)
                health = 0;

            if (healthText)
                healthText.text = health.ToString(CultureInfo.InvariantCulture);
            Debug.Log($"Ouch! I have {health} health left now.");
            //Debug.Log("Anth stinks ahah 22/02/26");
                                            
            if (health == 0)
            {
                Die();
            }
        }
    }
}
