using System.Collections.Generic;
using UnityEngine;

namespace Towers.Data
{
    [CreateAssetMenu(menuName = "Towers/TowerCore")]
    public class TowerCore : ScriptableObject
    {
        public List<ModuleSocket> sockets;
    }
}
