using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SmartIdTable.Editor
{
	public partial class IdRefDrawer
	{
		private static readonly float helpBoxHeight = EditorGUIUtility.singleLineHeight * 1.5f;
		
		private static void TryDrawLiteBox(Rect position, string currentValue, GUIContent label)
		{
			Rect rect = position;
			rect.height = helpBoxHeight;
			EditorGUI.HelpBox(rect, "Scene Objects aren't supported in Lite version", MessageType.Info);

			position.y += helpBoxHeight;
			position.height -= helpBoxHeight;

			EditorGUI.PrefixLabel(position, label);
			position.x += EditorGUIUtility.labelWidth;
			position.width -= EditorGUIUtility.labelWidth;

			GUI.enabled = false;
			GUI.Button(position, currentValue, EditorStyles.popup);
			GUI.enabled = true;
		}

		private static bool ShouldDisplayHelpBox(bool isSceneComponent) => isSceneComponent;

		private static bool IsSceneComponent(SerializedProperty property)
		{
			if (property.serializedObject.targetObject is not Component component)
				return false;

			return !(PrefabUtility.IsPartOfPrefabAsset(component) || CurrentSceneIsPrefabStage());

			bool CurrentSceneIsPrefabStage()
			{
				return PrefabStageUtility.GetCurrentPrefabStage() != null;
			}
		}
	}
}