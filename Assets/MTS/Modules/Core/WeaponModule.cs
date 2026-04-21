using MTS.Data;
using UnityEngine;

namespace MTS.Modules.Core
{
    public abstract class WeaponModule : TowerModule
    {
        public GameObject weaponPrefab;  // Weapon visuals
        public float damage = 5.0f;
        
        protected GameObject Instance;  // Held object for weapon prefabs
        protected bool UseRig = false;
        protected WeaponPrefabRig Rig;  // Prefab rig (can be null!)

        protected TowerContext Context;

        protected WeaponModule() : base(ModuleType.Weapon) {  // Call parent constructor (set module type)
        }

        protected WeaponPrefabRig FindWeaponRig()
        {
            if (UseRig && Rig != null)
            {
                return Rig;
            }

            Rig = weaponPrefab.GetComponent<WeaponPrefabRig>();  // Try and find the weapon rig
            if (Rig != null)
            {
                UseRig = true;
                return Rig;
            }
            
            Debug.LogWarning("WeaponModule: Could not find prefab rig, did you forget to add the component?");
            UseRig = false;
            return Rig;
        }
        
        public override void Install(TowerContext context)
        {
            Context = context;
        }

        public override void Uninstall(TowerContext context)
        {
            Context = null;
        }
    }
}
