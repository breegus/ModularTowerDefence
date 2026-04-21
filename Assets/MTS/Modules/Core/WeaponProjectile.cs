using UnityEngine;

namespace MTS.Modules.Core
{
    [CreateAssetMenu(menuName = "Towers/Modules/Weapons/Projectile")]
    public class WeaponProjectile : ScriptableObject
    {
        public GameObject projectilePrefab;
        public float damage;
        public float speed;
        public float lifetime;
    }
}
