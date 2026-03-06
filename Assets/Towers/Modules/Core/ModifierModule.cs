using Towers.Core;
using Towers.Data;
using UnityEngine.Animations;

namespace Towers.Modules.Core
{
    public abstract class ModifierModule : TowerModule
    {
        protected TowerContext Context;
        
        protected ModifierModule() : base(ModuleType.Modifier) {  // Call parent constructor (set module type)
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
