using Enemies;
using UnityEngine;
using Towers.Core;

namespace Towers.Data
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
