using UnityEditor;
using UnityEngine;

namespace FelineFellas.Editor
{
    [CustomPropertyDrawer(typeof(Coordinates))]
    public class CoordinatesDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Begin property drawing
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't indent child fields
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            float fieldWidth = position.width / 2f - 2f;
            var rowRect = new Rect(position.x, position.y, fieldWidth, position.height);
            var colRect = new Rect(position.x + fieldWidth + 4f, position.y, fieldWidth, position.height);

            // Get references to the properties
            var rowProp = property.FindPropertyRelative("_row");
            var colProp = property.FindPropertyRelative("_column");

            // Draw fields with labels
            EditorGUIUtility.labelWidth = 14f;

            EditorGUI.PropertyField(rowRect, rowProp, new GUIContent("R"));
            EditorGUI.PropertyField(colRect, colProp, new GUIContent("C"));

            // Restore indent level
            EditorGUI.indentLevel = indent;

            // End property drawing
            EditorGUI.EndProperty();
        }
    }
}