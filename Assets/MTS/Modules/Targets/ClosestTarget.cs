using MTS.Modules.Core;
using UnityEngine;

namespace MTS.Modules.Targets
{
    [CreateAssetMenu(menuName = "Towers/Modules/Targets/ClosestTarget")]
    public class ClosestTarget : TargetingModule
    {
        protected override void UpdateTarget()
        {
            var didHaveTarget = Context.CurrentTarget;
            var towerPosition = Context.TowerTransform.position;

            Context.CurrentTarget = Context.Enemies.GetMinBy(
                enemy => (enemy.transform.position - towerPosition).sqrMagnitude);

            if (Context.CurrentTarget)  // Target found / updated
                Context.Events.TargetFound(Context.CurrentTarget);

            if (didHaveTarget && !Context.CurrentTarget)  // Target lost
                Context.Events.TargetLost();
        }
    }
}
