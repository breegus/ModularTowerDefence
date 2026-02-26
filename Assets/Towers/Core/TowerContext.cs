using Enemies;
using UnityEngine;

namespace Towers.Core
{
    public class TowerContext
    {
        public Transform TowerTransform;
        public EnemyTracker Enemies;
        public TowerStatManager StatManager;
        public TowerEvents Events;
        public Enemy CurrentTarget;
    }
}
