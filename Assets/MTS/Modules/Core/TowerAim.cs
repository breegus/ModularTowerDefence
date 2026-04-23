using UnityEngine;

namespace MTS.Modules.Core
{
    public static class TowerAim
    {
        /// <summary>
        /// Aims an object towards the target
        /// </summary>
        public static bool DefaultAimTowards(Transform aimableObj, Vector3 target, float moveSpeed = 180.0f,
            float aimTolerance = 0.96f)
        {
            var dir = target - aimableObj.position;
            if (dir.sqrMagnitude <= Mathf.Epsilon)
            {
                return true;
            }

            var targetRotation = Quaternion.LookRotation(dir.normalized, aimableObj.up);
            aimableObj.rotation = Quaternion.RotateTowards(aimableObj.rotation, targetRotation, moveSpeed * Time.deltaTime);

            return IsWithinTolerance(aimableObj.forward, dir, aimTolerance);
        }
        
        /// <summary>
        /// Aims a WeaponPrefabRig at the target (using only assigned transforms and restrictions)
        /// </summary>
        public static bool AimRigTowards(WeaponPrefabRig rig, Vector3 target, float moveSpeed = 180.0f,
            float aimTolerance = 0.96f)
        {
            if (!rig.HasRestPose)
            {
                rig.CaptureRestPose();
            }

            if (!rig.yaw && !rig.pitch)
            {
                return DefaultAimTowards(rig.transform, target, moveSpeed, aimTolerance);
            }

            if (rig.yaw && rig.pitch && rig.yaw == rig.pitch)
            {
                return AimBothTowards(rig.yaw, rig.YawRestLocalRotation, target, moveSpeed, aimTolerance);
            }

            var yawAimed = !rig.yaw || AimYawTowards(rig.yaw, rig.YawRestLocalRotation, target, moveSpeed, aimTolerance);
            var pitchAimed = !rig.pitch || AimPitchTowards(rig.pitch, rig.PitchRestLocalRotation, target, moveSpeed, aimTolerance);

            return yawAimed && pitchAimed;
        }

        // Handle rig when both axis are the same model
        private static bool AimBothTowards(Transform pivotAim, Quaternion restLocalRotation, Vector3 target,
            float moveSpeed, float aimTolerance)
        {
            var referenceTransform = pivotAim.parent ? pivotAim.parent : pivotAim;
            var localTargetDirection = referenceTransform.InverseTransformDirection(target - pivotAim.position);
            if (localTargetDirection.sqrMagnitude <= Mathf.Epsilon)
            {
                return true;
            }

            var planarDistance = new Vector2(localTargetDirection.x, localTargetDirection.z).magnitude;
            var targetYaw = Mathf.Atan2(localTargetDirection.x, localTargetDirection.z) * Mathf.Rad2Deg;
            var targetPitch = Mathf.Atan2(-localTargetDirection.y, planarDistance) * Mathf.Rad2Deg;

            var targetLocalRotation = restLocalRotation * Quaternion.Euler(targetPitch, targetYaw, 0.0f);
            pivotAim.localRotation = Quaternion.RotateTowards(pivotAim.localRotation, targetLocalRotation,
                moveSpeed * Time.deltaTime);

            return IsWithinTolerance(pivotAim.forward, target - pivotAim.position, aimTolerance);
        }

        // Handle rig pitch model only
        private static bool AimPitchTowards(Transform pitchAim, Quaternion restLocalRotation, Vector3 target,
            float moveSpeed, float aimTolerance)
        {
            var referenceTransform = pitchAim.parent ? pitchAim.parent : pitchAim;
            var localTargetDirection = referenceTransform.InverseTransformDirection(target - pitchAim.position);
            var pitchPlaneDirection = new Vector3(0.0f, localTargetDirection.y, localTargetDirection.z);
            if (pitchPlaneDirection.sqrMagnitude <= Mathf.Epsilon)
            {
                return true;
            }

            var targetPitch = Mathf.Atan2(-pitchPlaneDirection.y, pitchPlaneDirection.z) * Mathf.Rad2Deg;
            var targetLocalRotation = restLocalRotation * Quaternion.Euler(targetPitch, 0.0f, 0.0f);
            pitchAim.localRotation = Quaternion.RotateTowards(pitchAim.localRotation, targetLocalRotation,
                moveSpeed * Time.deltaTime);

            return IsWithinTolerance(pitchAim.forward, target - pitchAim.position, aimTolerance);
        }

        // Handle yaw pitch model only
        private static bool AimYawTowards(Transform yawAim, Quaternion restLocalRotation, Vector3 target,
            float moveSpeed, float aimTolerance)
        {
            var referenceTransform = yawAim.parent ? yawAim.parent : yawAim;
            var localTargetDirection = referenceTransform.InverseTransformDirection(target - yawAim.position);
            var yawDirection = new Vector3(localTargetDirection.x, 0.0f, localTargetDirection.z);
            if (yawDirection.sqrMagnitude <= Mathf.Epsilon)
            {
                return true;
            }

            var targetYaw = Mathf.Atan2(yawDirection.x, yawDirection.z) * Mathf.Rad2Deg;
            var targetLocalRotation = restLocalRotation * Quaternion.Euler(0.0f, targetYaw, 0.0f);
            yawAim.localRotation = Quaternion.RotateTowards(yawAim.localRotation, targetLocalRotation,
                moveSpeed * Time.deltaTime);

            var currentForward = Vector3.ProjectOnPlane(yawAim.forward, referenceTransform.up);
            var worldYawDirection = Vector3.ProjectOnPlane(target - yawAim.position, referenceTransform.up);
            return IsWithinTolerance(currentForward, worldYawDirection, aimTolerance);
        }

        // Helper for determining aim tolerance
        private static bool IsWithinTolerance(Vector3 currentDirection, Vector3 targetDirection, float aimTolerance)
        {
            if (currentDirection.sqrMagnitude <= Mathf.Epsilon || targetDirection.sqrMagnitude <= Mathf.Epsilon)
            {
                return true;
            }

            var dot = Vector3.Dot(currentDirection.normalized, targetDirection.normalized);
            return dot > aimTolerance;
        }
    }
}
