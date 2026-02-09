using Towers.Core;
using UnityEngine;

namespace Towers.Modules.Core
{
    public abstract class TargetingModule : TowerModule
    {
        private TowerContext _context;
        
        public override void Install(TowerContext context)
        {
            _context = context;
            _context.Events.OnTick += UpdateTarget;
        }

        public override void Uninstall(TowerContext context)
        {
            _context.Events.OnTick -= UpdateTarget;
            _context = null;
        }
        
        protected abstract void UpdateTarget();
    }
}
