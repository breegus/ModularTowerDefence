using Towers.Core;
using Towers.Modules.Core;
using UnityEngine;

namespace Towers.Modules.Weapons
{
    [CreateAssetMenu(menuName = "Tower Modules/Weapons/Projectile")]
    public class ProjectileWeapon : WeaponModule
    {
        public float fireRate = 0.5f;
        public float damage = 5.0f;
        
        private float _fireRateTimer;

        public override void Install(TowerContext context)
        {
            base.Install(context);
            Context.Events.OnTick += TryFire;
        }

        public override void Uninstall(TowerContext context)
        {
            Context.Events.OnTick -= TryFire;
            base.Uninstall(context);
        }

        void TryFire()
        {
            _fireRateTimer -= Time.deltaTime;
            if (_fireRateTimer <= 0f) return;

            if (Context.CurrentTarget == null) return;  // No target set
            
            Fire(Context.CurrentTarget);
            _fireRateTimer = fireRate;

            //_fireRateTimer = Context.Stats.Get("FireRateMult") != null
            //    ? fireRate * Context.Stats.Get("FireRateMult")
            //    : fireRate;
        }

        void Fire(Enemy target)
        {
            //target.TakeDamage(damage);
            Context.Events.Hit(target);
        }
    }
}
