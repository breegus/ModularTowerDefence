using System.Collections.Generic;
using UnityEngine;

namespace Towers.Data
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "Towers/Core")]
    public class TowerCore : ScriptableObject
    {
        public List<ModuleSocket> sockets;
    }
}
