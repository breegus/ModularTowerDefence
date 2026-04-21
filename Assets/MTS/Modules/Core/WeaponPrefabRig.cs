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
    }
}
