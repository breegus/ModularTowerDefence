using UnityEngine;

namespace Towers.Modules.Core
{
    public static class TowerAim
    {
        public static bool AimTowards(Transform aimableObj, Vector3 target, float moveSpeed = 2.0f, float aimTolerance = 0.96f)
        {
            var dir = target - aimableObj.position;  // Aim towards target
            var rot = Quaternion.LookRotation(dir);
            
            aimableObj.rotation = Quaternion.Slerp(aimableObj.rotation, rot, moveSpeed * Time.deltaTime);
            
            var dirToTarget = (target - aimableObj.position).normalized;  // Check if aimed enough and return bool
            var dot = Vector3.Dot(aimableObj.forward, dirToTarget);
            
            return dot > aimTolerance;
        }
    }
}