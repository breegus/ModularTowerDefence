using UnityEngine;

namespace Towers.Core
{
    public class TowerGizmo : MonoBehaviour
    {
        public Color colour = Color.cyan;

        private void OnDrawGizmos()
        {
            Gizmos.color = colour;

            Gizmos.DrawSphere(transform.position, 0.15f);
        }
    }
}