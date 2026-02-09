using System;

namespace Towers.Core
{
    public class TowerEvents
    {
        public event Action OnTick;
        public event Action<Enemy> OnTargetFound;
        public event Action OnTargetLost;
        public event Action<Enemy> OnHit;
        public event Action OnKill;

        public void Tick() => OnTick?.Invoke();
        public void TargetFound(Enemy e) => OnTargetFound?.Invoke(e);
        public void TargetLost() => OnTargetLost?.Invoke();
        public void Hit(Enemy e) => OnHit?.Invoke(e);
        public void Kill() => OnKill?.Invoke();
    }
}
