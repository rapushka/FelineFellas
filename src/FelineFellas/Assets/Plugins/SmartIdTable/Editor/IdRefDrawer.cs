#define ID_TABLE_SEACH_WINDOW
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace SmartIdTable.Editor
{
	[CustomPropertyDrawer(typeof(IdRefAttribute))]
	public partial class IdRefDrawer : PropertyDrawer
	{
		private static readonly Regex specialValueRegex = new(@"(?!\$)(.+)");

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType is not SerializedPropertyType.String)
			{
				EditorGUI.LabelField(position, label.text, "ID field must be of string type.");
				return;
			}

			bool isSceneComponent = IsSceneComponent(property);

			IdRefAttribute idRefAttribute = (IdRefAttribute)attribute;
			IdTable idTable = IdTable.Get(idRefAttribute.TableName);
			if (idTable == null)
			{
				EditorGUI.LabelField(position, label.text, $"Can't find ID Table '{idRefAttribute.TableName}'.");
				return;
			}

			string currentValue = string.IsNullOrEmpty(property.stringValue) ? SpecialIdValues.NONE : property.stringValue;
			
			if (ShouldDisplayHelpBox(isSceneComponent))
			{
				TryDrawLiteBox(position, currentValue, label);
				return;
			}

			bool hasSpecialValue = Utils.IsSpecialValue(currentValue);
			string[] options = GetDisplayedOptions(idRefAttribute, idTable);

			var style = new GUIStyle(EditorStyles.popup);
			
			bool hasReferenceToTable;
			if (hasSpecialValue)
			{
				currentValue = ParseSpecialValue(currentValue);
				hasReferenceToTable = false;
			}
			else if (!string.IsNullOrEmpty(currentValue) && !isSceneComponent && !idTable.ContainsReferenceTo(property))
			{
				style.normal.textColor = Color.red;
				currentValue = $"{currentValue} (Missing Reference)";
				hasReferenceToTable = false;
			}
			else
			{
				hasReferenceToTable = true;
			}

#if ID_TABLE_SEACH_WINDOW
			EditorGUI.PrefixLabel(position, label);
			position.x += EditorGUIUtility.labelWidth;
			position.width -= EditorGUIUtility.labelWidth;

			if (GUI.Button(position, new GUIContent(currentValue), style))
			{
				SetupSearchWindow(property, options, idTable, hasReferenceToTable);
			}
#else
			EditorGUI.BeginChangeCheck();

			int initialIndex = Array.IndexOf(options, currentValue);
			int index = EditorGUI.Popup(position, label.text, initialIndex, options);
			if (hasSpecialValue)
				DrawSpecialValue(position, currentValue);

			if (EditorGUI.EndChangeCheck())
			{
				UpdatePropertyValue(property, options[index]);
			}
#endif
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float baseHeight = base.GetPropertyHeight(property, label);
			if (property.propertyType != SerializedPropertyType.String)
				return baseHeight;
			
			return ShouldDisplayHelpBox(IsSceneComponent(property)) ? baseHeight + helpBoxHeight : baseHeight;
		}

		public static string ParseSpecialValue(string value) => specialValueRegex.Match(value).Value;

		private static string[] GetDisplayedOptions(IdRefAttribute idAttribute, IdTable idTable)
		{
			var idList = new List<string>(idTable.Ids.Where(MatchesStartsWith).Where(MatchesEndsWith).OrderBy(s => s));
			idList.Insert(0, SpecialIdValues.NONE);
			return idList.ToArray();

			bool MatchesStartsWith(string id) => idAttribute.StartsWith.Length == 0 || idAttribute.StartsWith.Any(id.StartsWith);
			bool MatchesEndsWith(string id) => idAttribute.EndsWith.Length == 0 || idAttribute.EndsWith.Any(id.EndsWith);
		}
	}
}