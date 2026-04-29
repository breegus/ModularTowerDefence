using MTS.Data;
using UnityEngine;

namespace MTS.Modules.Core
{
    public abstract class WeaponModule : TowerModule
    {
        public GameObject weaponPrefab;  // Weapon visuals
        public float damage = 5.0f;
        
        protected GameObject Instance;  // Held object for weapon prefabs
        protected bool UseRig;
        protected WeaponPrefabRig Rig;  // Prefab rig (can be null!)

        protected TowerContext Context;

        protected WeaponModule() : base(ModuleType.Weapon) {  // Call parent constructor (set module type)
        }

        // Find weapon rig and set as active if valid
        protected void FindWeaponRig()
        {
            if (UseRig && Rig != null) return;

            var rigSource = Instance ? Instance : weaponPrefab;
            Rig = rigSource.GetComponent<WeaponPrefabRig>();
            if (Rig != null)
            {
                UseRig = true;
                Rig.CaptureRestPose();
                Debug.Log("WeaponModule: Found weapon rig");
                return;
            }
            
            Debug.LogWarning("WeaponModule: Could not find prefab rig, did you forget to add the component?");
            UseRig = false;
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
