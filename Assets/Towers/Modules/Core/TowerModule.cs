using Towers.Core;
using Towers.Data;
using UnityEngine;

namespace Towers.Modules.Core
{
    public enum ModuleType
    {
        Weapon,
        Targeting,
        Modifier
    };
    
    public abstract class TowerModule : ScriptableObject
    {
        public ModuleType type;

        protected TowerModule(ModuleType type)
        {
            this.type = type;
        }
        public abstract void Install(TowerContext context);
        public abstract void Uninstall(TowerContext context);
    }
}
