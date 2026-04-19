using MTS.Data;

namespace MTS.Modules.Core
{
    public abstract class TargetingModule : TowerModule
    {
        protected TowerContext Context;
        
        protected TargetingModule() : base(ModuleType.Targeting) {  // Call parent constructor (set module type)
        }
        
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
