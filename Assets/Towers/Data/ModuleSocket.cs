using System;
using Towers.Modules.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Towers.Data
{
    public enum SocketType
    {
        Weapon,
        Targeting,
        Modifier
    };
    
    [Serializable]
    public class ModuleSocket
    {
        public SocketType type;
        public TowerModule module;
    }
}
