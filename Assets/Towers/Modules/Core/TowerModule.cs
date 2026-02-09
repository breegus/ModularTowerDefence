using Towers.Core;
using UnityEngine;

namespace Towers.Modules.Core
{
    public abstract class TowerModule : ScriptableObject
    {
        public abstract void Install(TowerContext context);
        public abstract void Uninstall(TowerContext context);
    }
}
