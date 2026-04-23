using MTS.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace Editor
{
    [CustomEditor(typeof(TowerRuntime))]
    public class TowerRuntimeEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var tower = (TowerRuntime)target;  // Access tower runtime
            
            var t = tower.transform;
            var worldPos = t.TransformPoint(tower.weaponOffset);  // World-space location
            var handleRot = t.rotation;  // Keep handle aligned to tower
            
            EditorGUI.BeginChangeCheck();

            var newWorldPos = Handles.PositionHandle(worldPos, handleRot);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(tower, "Move Weapon Offset");

                tower.weaponOffset = t.InverseTransformPoint(newWorldPos);  // Convert to local offset
            };
            
            Handles.color = Color.red;
            Handles.SphereHandleCap(0, worldPos, Quaternion.identity, 0.08f, EventType.Repaint);
        }
    }
}
