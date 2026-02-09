using Towers.Core;
using UnityEngine;

namespace Towers.Modules.Core
{
    public abstract class TargetingModule : TowerModule
    {
        protected TowerContext Context;
        
        public override void Install(TowerContext context)
        {
            Context = context;
            Context.Events.OnTick += UpdateTarget;
        }

        public override void Uninstall(TowerContext context)
        {
            Context.Events.OnTick -= UpdateTarget;
            Context = null;
        }
        
        protected abstract void UpdateTarget();
    }
}
