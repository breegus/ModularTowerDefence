using Towers.Core;

namespace Towers.Modules.Core
{
    public abstract class ModifierModule : TowerModule
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
