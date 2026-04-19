using Enemies;
using MTS.Data;
using MTS.Modules.Core;
using UnityEngine;

namespace MTS.Modules.Weapons
{
    [CreateAssetMenu(menuName = "Towers/Modules/Weapons/ProjectileWeapon")]
    public class ProjectileWeapon : WeaponModule
    {
        public float fireRate = 0.5f;
        public float damage = 5.0f;
        
        public GameObject weaponPrefab;  // Weapon visuals
        public GameObject projectilePrefab;  // Projectile visual

        private GameObject _instance;  // Held object for weapon prefabs
        private float _fireRateTimer;

        public override void Install(TowerContext context)
        {
            base.Install(context);

            _instance = Instantiate(weaponPrefab, Context.TowerTransform, false);
            _instance.transform.position = Context.TowerTransform.TransformPoint(Context.WeaponOffset);

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
            if (!Context.CurrentTarget) return;  // No target set

            if (TowerAim.AimTowards(_instance.transform, Context.CurrentTarget.transform.position))
            {
                _fireRateTimer -= Time.deltaTime;
                if (_fireRateTimer >= 0f) return;
                            
                Fire(Context.CurrentTarget);
                
                _fireRateTimer = Context.StatManager.Get("FireRate");
            };
        }

        private void Fire(Enemy target)
        {
            Debug.DrawLine(_instance.transform.position, target.transform.position, Color.yellow, 0.2f, false);
            target.TakeDamage(Context.StatManager.Get("Damage"));
            Context.Events.Hit(target);
        }
    }
}
