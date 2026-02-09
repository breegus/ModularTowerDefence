using Towers.Modules.Core;

namespace Towers.Modules.Targets
{
    public class ClosestTarget : TargetingModule
    {
        protected override void UpdateTarget()
        {
            var didHaveTarget = Context.CurrentTarget;
            Context.CurrentTarget = Context.Enemies.GetClosestTo(Context.TowerTransform.position);
            
            if (Context.CurrentTarget)  // Target found / updated
                Context.Events.TargetFound(Context.CurrentTarget);

            if (didHaveTarget && !Context.CurrentTarget)  // Target lost
                Context.Events.TargetLost();
        }
    }
}
