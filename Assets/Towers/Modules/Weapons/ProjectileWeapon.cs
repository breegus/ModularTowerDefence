using Enemies;
using Towers.Core;
using Towers.Modules.Core;
using UnityEngine;

namespace Towers.Modules.Weapons
{
    [CreateAssetMenu(menuName = "Towers/Modules/Weapons/ProjectileWeapon")]
    public class ProjectileWeapon : WeaponModule
    {
        public float fireRate = 0.5f;
        public float damage = 5.0f;
        
        public GameObject weaponPrefab;  // Weapon visuals
        public Vector3 weaponOffset;
        public Vector3 weaponRotationOffset;
        
        public GameObject projectilePrefab;  // Projectile visual

        private GameObject _instance;  // Held object for weapon prefabs
        private float _fireRateTimer;

        public override void Install(TowerContext context)
        {
            base.Install(context);
            Context.Events.OnTick += TryFire;
            Context.StatManager.SetBase("FireRate", fireRate);
            Context.StatManager.SetBase("Damage", damage);
        }

        public override void Uninstall(TowerContext context)
        {
            Context.Events.OnTick -= TryFire;
            base.Uninstall(context);
        }

        private void TryFire()
        {
            _fireRateTimer -= Time.deltaTime;
            if (_fireRateTimer >= 0f) return;

            if (!Context.CurrentTarget) return;  // No target set
            
            Fire(Context.CurrentTarget);

            _fireRateTimer = Context.StatManager.Get("FireRate");
        }

        private void Fire(Enemy target)
        {
            Debug.DrawLine(Context.TowerTransform.position, target.transform.position, Color.yellow, 0.2f);
            target.TakeDamage(Context.StatManager.Get("Damage"));
            Context.Events.Hit(target);
        }
    }
}
