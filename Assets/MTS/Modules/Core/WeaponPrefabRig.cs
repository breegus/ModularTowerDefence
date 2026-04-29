using UnityEngine;

namespace MTS.Modules.Core
{
    public class WeaponPrefabRig : MonoBehaviour
    {
        [Tooltip("Object to be rotated by pitch (up and down)")]
        public Transform pitch;
        [Tooltip("Object to be rotated by yaw (left and right)")]
        public Transform yaw;
        [Tooltip("Where the projectile will spawn from (if in use)")]
        public Transform projectileOffset;

        [System.NonSerialized] public Quaternion PitchRestLocalRotation;
        [System.NonSerialized] public Quaternion YawRestLocalRotation;
        [System.NonSerialized] public bool HasRestPose;

        /// <summary>
        /// Finds the default rotation to take into account when aiming the rig.
        /// </summary>
        public void CaptureRestPose()
        {
            if (pitch)
            {
                PitchRestLocalRotation = pitch.localRotation;
            }

            if (yaw)
            {
                YawRestLocalRotation = yaw.localRotation;
            }

            HasRestPose = true;
        }
    }
}
