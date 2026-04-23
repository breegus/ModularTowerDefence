using Enemies;
using MTS.Data;
using MTS.Modules.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace MTS.Modules.Weapons
{
    [CreateAssetMenu(menuName = "Towers/Modules/Weapons/ProjectileWeapon")]
    public class ProjectileWeapon : WeaponModule
    {
        public float fireRate = 0.5f;
        
        public GameObject projectilePrefab;  // Projectile visual
        
        private float _fireRateTimer;

        public override void Install(TowerContext context)
        {
            base.Install(context);

            Instance = Instantiate(weaponPrefab, Context.TowerTransform, false);
            Instance.transform.position = Context.TowerTransform.TransformPoint(Context.WeaponOffset);

            FindWeaponRig();

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
            
            var canFire = UseRig ? TowerAim.AimRigTowards(Rig, Context.CurrentTarget.transform.position) : 
                TowerAim.DefaultAimTowards(Instance.transform, Context.CurrentTarget.transform.position);
            
            Debug.DrawLine(UseRig ? Rig.projectileOffset.position : Instance.transform.position, Context.CurrentTarget.transform.position, Color.red, 0.01f);

            if (canFire)
            {
                _fireRateTimer -= Time.deltaTime;
                if (_fireRateTimer >= 0f) return;
                            
                Fire(Context.CurrentTarget);
                
                _fireRateTimer = Context.StatManager.Get("FireRate");
            };
        }

        private void Fire(Enemy target)
        {
            Debug.DrawLine(UseRig ? Rig.projectileOffset.position : Instance.transform.position, target.transform.position, Color.yellow, 0.2f, false);
            target.TakeDamage(Context.StatManager.Get("Damage"));
            Context.Events.Hit(target);
        }
    }
}
