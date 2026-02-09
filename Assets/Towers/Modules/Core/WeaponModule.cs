using Towers.Core;
using UnityEngine;

namespace Towers.Modules.Core
{
    public abstract class WeaponModule : TowerModule
    {
        private TowerContext _context;

        public override void Install(TowerContext context)
        {
            _context = context;
        }

        public override void Uninstall(TowerContext context)
        {
            _context = null;
        }
    }
}
