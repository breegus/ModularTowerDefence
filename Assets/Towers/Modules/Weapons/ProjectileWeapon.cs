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
        
        private float _fireRateTimer;

        public override void Install(TowerContext context)
        {
            base.Install(context);
            Context.Events.OnTick += TryFire;
            Context.Stats.SetBase("FireRate", fireRate);
            Context.Stats.SetBase("Damage", damage);
        }

        public override void Uninstall(TowerContext context)
        {
            Context.Events.OnTick -= TryFire;
            base.Uninstall(context);
        }

        void TryFire()
        {
            _fireRateTimer -= Time.deltaTime;
            if (_fireRateTimer >= 0f) return;

            if (!Context.CurrentTarget) return;  // No target set
            
            Fire(Context.CurrentTarget);

            _fireRateTimer = Context.Stats.Get("FireRate");
        }

        void Fire(Enemy target)
        {
            target.TakeDamage(Context.Stats.Get("Damage"));
            Context.Events.Hit(target);
        }
    }
}
