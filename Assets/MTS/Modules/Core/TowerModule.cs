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
            Type = type;
        }
        
        /// <summary>
        /// Called when the tower needs to use the module
        /// </summary>
        public abstract void Install(TowerContext context);
        
        /// <summary>
        /// Called when the tower is done using the module
        /// </summary>
        public abstract void Uninstall(TowerContext context);
    }
}
