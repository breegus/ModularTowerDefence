using Towers.Core;
using Towers.Data;
using UnityEngine;

namespace Towers.Modules.Core
{
    public abstract class WeaponModule : TowerModule
    {
        protected TowerContext Context;
        
        protected WeaponModule() : base(ModuleType.Weapon) {  // Call parent constructor (set module type)
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
