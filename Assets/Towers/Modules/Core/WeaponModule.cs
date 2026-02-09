using Towers.Core;
using UnityEngine;

namespace Towers.Modules.Core
{
    public abstract class WeaponModule : TowerModule
    {
        protected TowerContext Context;
        
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
