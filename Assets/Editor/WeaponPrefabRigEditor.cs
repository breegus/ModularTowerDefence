using MTS.Modules.Core;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(WeaponPrefabRig))]
    public class WeaponPrefabRigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var rig = (WeaponPrefabRig)target;

            EditorGUILayout.HelpBox(
                "Recommended hierarchy: Root -> YawObject -> PitchObject" + 
                "Leave an axis unassigned to fall back to root aiming for that axis.",
                MessageType.Info);

            if (rig.pitch && rig.pitch == rig.yaw)
            {
                EditorGUILayout.HelpBox(
                    "Pitch and yaw are assigned to the same transform. This now uses single-pivot combined aiming instead of separate axis.",
                    MessageType.Info);
            }

            DrawDefaultInspector();
        }

        private void OnSceneGUI()
        {
            var rig = (WeaponPrefabRig)target;  // Access weapon prefab rig
            if (!rig.projectileOffset)  // If offset is valid
            {
                return;
            }
            
            var t = rig.projectileOffset;
            var worldPos = t.position;  // World-space location
            var handleRot = t.rotation;  // Keep handle aligned to rig

            EditorGUI.BeginChangeCheck();

            var newWorldPos = Handles.PositionHandle(worldPos, handleRot);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(rig, "Move Projectile Offset");

                rig.projectileOffset.position = newWorldPos;  // Convert to local offset
            };
    
            // Draw gizmo
            Handles.color = Color.red;
            Handles.SphereHandleCap(0, worldPos, Quaternion.identity, 0.08f, EventType.Repaint);
        }
    }
}
