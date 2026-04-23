using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MTS.Data;
using MTS.Modules.Core;
using UnityEngine;

namespace MTS.Core
{
    public class TowerRuntime : MonoBehaviour
    {
        public TowerCore core;

        public Vector3 weaponOffset = Vector3.forward;

        private TowerContext _context;

        private TowerModule _targetModule;
        private TowerModule _weaponModule;
        private List<TowerModule> _modifierModules = new();

        private void Start()
        {
            _context = new TowerContext
            {
                TowerTransform = transform,
                WeaponOffset = weaponOffset,
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

        /// <summary>
        /// Installs and begins using a module
        /// </summary>
        public void InstallModule(TowerModule newModule)
        {
            if (!newModule) return;

            Debug.Log($"TowerRuntime: Installing new {newModule.Type.ToString()} module.");
            
            var instance = Instantiate(newModule);  // Create new module
            
            switch (newModule.Type)
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

        /// <summary>
        /// Removes and stops using a module safely
        /// </summary>
        public void UninstallModule([CanBeNull] TowerModule oldModule)
        {
            if (!oldModule) return;
            
            Debug.Log($"TowerRuntime: Uninstalling {oldModule.Type.ToString()} module.");

            oldModule.Uninstall(_context);
            
            switch (oldModule.Type)
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

        /// <summary>
        /// Swaps the current module for a new one
        /// </summary>
        public void ReplaceModule(TowerModule oldModule, TowerModule newModule)
            {
                if (newModule.Type != oldModule.Type)  // Sanity check module types match to prevent errors
                {
                    Debug.LogError($"TowerRuntime: Module type mismatch when replacing! ({oldModule.Type.ToString()}) -> {newModule.Type.ToString()})");
                    return;
                }
                
                oldModule.Uninstall(_context);  // Uninstall and delete old module
                Destroy(oldModule);
                
                var instance = Instantiate(newModule);
                
                switch (oldModule.Type)
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
            if (core && core.weaponModule && core.weaponModule.weaponPrefab && !Application.isPlaying)
            {
                Gizmos.color = Color.cyan;
                var weaponPrefab = core.weaponModule.weaponPrefab;
                var rootMatrix = Matrix4x4.TRS(
                    transform.TransformPoint(weaponOffset), 
                    transform.rotation, 
                    Vector3.one);
                
                foreach (var meshFilter in weaponPrefab.GetComponentsInChildren<MeshFilter>())
                {
                    if (!meshFilter.sharedMesh) continue;
                    var localToPrefabRoot =
                        weaponPrefab.transform.worldToLocalMatrix *
                        meshFilter.transform.localToWorldMatrix;
                    
                    Gizmos.matrix = rootMatrix * localToPrefabRoot;
                    Gizmos.DrawMesh(meshFilter.sharedMesh);
                }
                Gizmos.matrix = Matrix4x4.identity;
            }
            
            
            if (_context == null || !_context.CurrentTarget) return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _context?.StatManager.Get("Range") ?? 5f);
        }

        /*public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.lightSkyBlue;
            Gizmos.DrawWireSphere(transform.TransformPoint(weaponOffset), 0.125f);
        }*/
    }
}
