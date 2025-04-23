using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SmartIdTable.Editor
{
	public partial class IdRefDrawer
	{
		private static void SetupSearchWindow(SerializedProperty property, string[] options, IdTable idTable, bool hasReferenceToTable)
		{
			var context = new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition));
			var idRefContext = new IdRefContext(options, property, idTable, hasReferenceToTable);
			var searchWindow = IdRefSearchWindow.Create(idRefContext, UpdatePropertyValue);

			SearchWindow.Open(context, searchWindow);
		}

		private static void UpdatePropertyValue(IdRefContext context, string newValue)
		{
			SerializedProperty property = context.Property;
			IdTable idTable = context.IdTable;

			string oldValue = property.stringValue;
			if (oldValue == newValue && context.HasReferenceToTable)
				return;

			Object targetObject = property.serializedObject.targetObject;
			var refData = RefData.Create(targetObject, property.propertyPath);

			if (newValue != SpecialIdValues.NONE && !idTable.AddReferenceToId(newValue, refData))
				throw new ArgumentException($"Couldn't add reference to ID '{newValue}'", nameof(newValue));

			// Remove ref from previous id
			if (oldValue != SpecialIdValues.NONE && context.HasReferenceToTable)
				idTable.RemoveReferenceFromId(oldValue, refData);

			property.stringValue = newValue;
			property.serializedObject.ApplyModifiedProperties();
		}
	}
}