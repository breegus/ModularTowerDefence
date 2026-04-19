using Enemies;
using MTS.Core;
using UnityEngine;

namespace MTS.Data
{
    public class TowerContext
    {
        public Transform TowerTransform;
        public Vector3 WeaponOffset;
        public EnemyTracker Enemies;
        public TowerStatManager StatManager;
        public TowerEvents Events;
        public Enemy CurrentTarget;
    }
}
