using System.Collections.Generic;
using System.Linq;
using Towers.Data;
using Towers.Modules.Core;
using UnityEngine;

namespace Towers.Core
{
    public class TowerRuntime : MonoBehaviour
    {
        public TowerCore core;

        private TowerContext _context;
        private readonly List<TowerModule> _modules = new();

        private void Start()
        {
            _context = new TowerContext
            {
                TowerTransform = transform,
                Enemies = FindFirstObjectByType<EnemyTracker>(),  // Get tracker from scene
                StatManager = new TowerStatManager(),
                Events = new TowerEvents()
            };

            Build();
        }

        private void Build()
        {
            if (!core.sockets.Any()) return;  // Do nothing if empty
            
            Debug.Log($"Building {core.sockets.Count()} sockets: {core.sockets}");
            
            // Create and install each module to sockets
            foreach (var instance in core.sockets.Select(Instantiate))
            {
                //if (!instance) continue;
                instance.Install(_context);
                _modules.Add(instance);
            }
        }

        private void Update()
        {
            _context.Events.Tick();  // Update ticked modules
        }

        public void ReplaceModule(TowerModule oldModule, TowerModule newModule)
        {
            oldModule.Uninstall(_context);  // Uninstall and delete old module
            
            var moduleIndex = _modules.IndexOf(oldModule);  // Remember old index
            _modules.Remove(oldModule);
            Destroy(oldModule);
            
            var instance = Instantiate(newModule);  // Create and install new module
            _modules.Insert(moduleIndex, instance);  // Insert module into old index (replace)
            instance.Install(_context);
            _modules.Add(instance);
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
