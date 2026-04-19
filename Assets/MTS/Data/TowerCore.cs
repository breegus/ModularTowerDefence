using System.Collections.Generic;
using MTS.Modules.Core;
using UnityEngine;

namespace MTS.Data
{
    [CreateAssetMenu(menuName = "Towers/TowerCore")]
    public class TowerCore : ScriptableObject
    {
        public TargetingModule targetingModule;
        public WeaponModule weaponModule;
        public List<ModifierModule> modifierModules;
    }
}
