using Enemies;
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
            
            Context.CurrentTarget = GetClosestTo(Context.TowerTransform.position);
            
            if (Context.CurrentTarget)  // Target found / updated
                Context.Events.TargetFound(Context.CurrentTarget);

            if (didHaveTarget && !Context.CurrentTarget)  // Target lost
                Context.Events.TargetLost();
        }
        
        private Enemy GetClosestTo(Vector3 pos)
        {
            Enemy closest = null;
            var minDist = float.MaxValue;

            foreach (var enemy in Context.Enemies.GetAll())
            {
                var dist = Vector3.Distance(pos, enemy.transform.position);
                
                if (dist > minDist) continue;  // if dist < minDist then set as new closest
                minDist = dist;
                closest = enemy;
            }

            return closest;
        }
    }
}
