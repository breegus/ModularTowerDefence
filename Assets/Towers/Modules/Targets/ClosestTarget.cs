using Towers.Modules.Core;

namespace Towers.Modules.Targets
{
    public class ClosestTarget : TargetingModule
    {
        protected override void UpdateTarget()
        {
            var didHaveTarget = Context.CurrentTarget != null;
            //Context.CurrentTarget = Context.Enemies.GetClosest(Transform.position);
            
            if (Context.CurrentTarget != null)  // Target found / updated
                Context.Events.TargetFound(Context.CurrentTarget);

            if (didHaveTarget && Context.CurrentTarget == null)  // Target lost
                Context.Events.TargetLost();
        }
    }
}
