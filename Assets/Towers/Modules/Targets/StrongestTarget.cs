using Towers.Modules.Core;
using UnityEngine;

namespace Towers.Modules.Targets
{
    [CreateAssetMenu(menuName = "Towers/Modules/Targets/StrongestTarget")]
    public class StrongestTarget : TargetingModule
    {
        protected override void UpdateTarget()
        {
            var didHaveTarget = Context.CurrentTarget;
            
            Context.CurrentTarget = Context.Enemies.GetFirstDescending(e => e.health);
            
            if (Context.CurrentTarget)  // Target found / updated
                Context.Events.TargetFound(Context.CurrentTarget);

            if (didHaveTarget && !Context.CurrentTarget)  // Target lost
                Context.Events.TargetLost();
        }
    }
}
