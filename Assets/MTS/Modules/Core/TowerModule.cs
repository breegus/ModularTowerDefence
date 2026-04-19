using MTS.Data;
using UnityEngine;

namespace MTS.Modules.Core
{
    public enum ModuleType
    {
        Weapon,
        Targeting,
        Modifier
    };
    
    public abstract class TowerModule : ScriptableObject
    {
        public readonly ModuleType Type;

        protected TowerModule(ModuleType type)
        {
            this.Type = type;
        }
        public abstract void Install(TowerContext context);
        public abstract void Uninstall(TowerContext context);
    }
}
