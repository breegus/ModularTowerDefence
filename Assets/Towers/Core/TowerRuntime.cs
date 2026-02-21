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
        private List<TowerModule> _modules = new();

        private void Start()
        {
            _context = new TowerContext
            {
                TowerTransform = transform,
                Enemies = FindFirstObjectByType<EnemyTracker>(),  // Get tracker from scene
                Stats = new TowerStats(),
                Events = new TowerEvents()
            };

            Build();
        }

        private void Build()
        {
            // Create and install each module to sockets
            foreach (var instance in core.sockets.Select(socket => Instantiate(socket.module)))
            {
                instance.Install(_context);
                _modules.Add(instance);
            }
        }

        private void Update()
        {
            _context.Events.Tick();  // Update ticked modules
        }

        public void ReplaceModule(ModuleSocket socket, TowerModule newModule)
        {
            var oldModule = socket.module;  // Uninstall and delete old module
            oldModule.Uninstall(_context);
            _modules.Remove(oldModule);
            Destroy(oldModule);

            var instance = Instantiate(newModule);  // Create and install new module
            socket.module = instance;
            instance.Install(_context);
            _modules.Add(instance);
        }

        public void OnDrawGizmos()
        {
            if (_context == null || !_context.CurrentTarget) return;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _context.CurrentTarget.transform.position);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _context?.Stats.Get("Range") ?? 5f);
        }
    }
}
