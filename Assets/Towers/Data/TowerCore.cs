using System.Collections.Generic;
using Towers.Modules.Core;
using UnityEngine;

namespace Towers.Data
{
    [CreateAssetMenu(menuName = "Towers/TowerCore")]
    public class TowerCore : ScriptableObject
    {
        public TargetingModule targetingModule;
        public WeaponModule weaponModule;
        public List<ModifierModule> modifierModules;
    }
}
