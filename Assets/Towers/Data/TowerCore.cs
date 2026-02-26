using System.Collections.Generic;
using Towers.Modules.Core;
using UnityEngine;

namespace Towers.Data
{
    [CreateAssetMenu(menuName = "Towers/TowerCore")]
    public class TowerCore : ScriptableObject
    {
        public List<TowerModule> sockets;
    }
}
