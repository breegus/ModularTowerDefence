using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Towers.Data;
using Towers.Modules.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace Towers.Core
{
    public class TowerRuntime : MonoBehaviour
    {
        public TowerCore core;

        private TowerContext _context;

        private TowerModule _targetModule;
        private TowerModule _weaponModule;
        private List<TowerModule> _modifierModules = new();

        private void Start()
        {
            _context = new TowerContext
            {
                TowerTransform = transform,
                Enemies = FindFirstObjectByType<EnemyTracker>(), // Get tracker from scene
                StatManager = new TowerStatManager(),
                Events = new TowerEvents()
            };

            if (!_context.Enemies)
            {
                Debug.LogError("TowerRuntime: Couldn't find an EnemyTracker. Please make sure there is a single instance in the scene!");
            }

            Build();
        }

        private void Build()
        {
            Debug.Log("TowerRuntime: Building Tower Modules...");

            InstallModule(core.targetingModule);  // Targeting & Weapon modules
            InstallModule(core.weaponModule);

            if (!core.modifierModules.Any()) return;  // Modifier module(s)
            Debug.Log($"TowerRuntime: Installing {core.modifierModules.Count()} modifiers: {core.modifierModules}");
            
            foreach (var modifier in core.modifierModules)
            {
                InstallModule(modifier);
            }
        }

        private void Update()
        {
            _context.Events.Tick(); // Update ticked modules
        }

        public void InstallModule(TowerModule newModule)
        {
            if (!newModule) return;

            Debug.Log($"TowerRuntime: Installing new {newModule.type.ToString()} module.");
            
            var instance = Instantiate(newModule);  // Create new module
            
            switch (newModule.type)
            {
                case ModuleType.Weapon:  // Weapon
                    _weaponModule = instance;
                    break;
                case ModuleType.Targeting:  // Targeting
                    _targetModule = instance;
                    break;
                case ModuleType.Modifier:  // Modifier
                    _modifierModules.Add(instance);
                    break;
                default:
                    Debug.LogWarning("TowerRuntime: Module type unknown when installing module.");
                    return;
            }
            
            instance.Install(_context);
        }

        public void UninstallModule([CanBeNull] TowerModule oldModule)
        {
            if (!oldModule) return;
            
            Debug.Log($"TowerRuntime: Uninstalling {oldModule.type.ToString()} module.");

            oldModule.Uninstall(_context);
            
            switch (oldModule.type)
            {
                case ModuleType.Weapon:  // Weapon
                    _weaponModule = null;
                    break;
                case ModuleType.Targeting:  // Targeting
                    _targetModule = null;
                    break;
                case ModuleType.Modifier:  // Modifier
                    _modifierModules.Remove(oldModule);
                    break;
                default:
                    Debug.LogWarning("TowerRuntime: Module type unknown when uninstalling module.");
                    return;
            }
            
            Destroy(oldModule);
        }

        public void ReplaceModule(TowerModule oldModule, TowerModule newModule)
            {
                if (newModule.type != oldModule.type)  // Sanity check module types match to prevent errors
                {
                    Debug.LogError($"TowerRuntime: Module type mismatch when replacing! ({oldModule.type.ToString()}) -> {newModule.type.ToString()})");
                    return;
                }
                
                oldModule.Uninstall(_context);  // Uninstall and delete old module
                Destroy(oldModule);
                
                var instance = Instantiate(newModule);
                
                switch (oldModule.type)
                {
                    case ModuleType.Weapon:  // Replace weapon module
                        _weaponModule = instance;
                        break;
                    case ModuleType.Targeting:  // Replace target module
                        _targetModule = instance;
                        break;
                    case ModuleType.Modifier:  // Replace modifier module
                        var moduleIndex = _modifierModules.IndexOf(oldModule);  // Remember old index
                        _modifierModules[moduleIndex] = instance;  // Insert module into old index (replacing old)
                        break;
                    default:
                        Debug.LogWarning("TowerRuntime: Module type unknown when replacing module.");
                        return;
                }
                
                instance.Install(_context);
            }

        public void OnDrawGizmos()
        {
            if (_context == null || !_context.CurrentTarget) return;
            Gizmos.color = Color.red;
            Debug.Log(_context.CurrentTarget.transform.position);
            Gizmos.DrawLine(transform.position, _context.CurrentTarget.transform.position);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _context?.StatManager.Get("Range") ?? 5f);
        }
    }
}
