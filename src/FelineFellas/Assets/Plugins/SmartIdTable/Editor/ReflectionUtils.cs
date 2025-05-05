using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;

namespace SmartIdTable.Editor
{
	public static class ReflectionUtils
	{
		public static FieldInfo AssetRefToFieldInfo(RefData refData, out object fieldHolder) =>
			PropertyPathToFieldInfo(refData.GetTargetObject(), refData.PropertyPath, out fieldHolder);

		public static FieldInfo GetSerializedPropertyFieldInfo(this SerializedObject serObj, string propertyPath, out object fieldHolder) =>
			PropertyPathToFieldInfo(serObj.targetObject, propertyPath, out fieldHolder);

		private static FieldInfo PropertyPathToFieldInfo(object targetObject, string propertyPath, out object fieldHolder)
		{
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

			string[] splitPath = propertyPath.Split('.');
			object current = targetObject;
			for (int i = 0; i < splitPath.Length - 1; i++)
			{
				string propertyName = splitPath[i];
				if (propertyName == "Array")
				{
					var list = current as IList;

					if (list is null)
						continue;

					string idxProperty = splitPath[++i];
					string match = Regex.Match(idxProperty, @"data\[(\d+)\]").Groups[1].Value;
					int idx = int.Parse(match);

					current = list[idx];
				}
				else
				{
					FieldInfo field = current.GetType().GetField(propertyName, flags);

					if (field is null)
						continue;

					current = field.GetValue(current);
				}
			}

			string fieldName = splitPath[^1];
			fieldHolder = current;

			FieldInfo targetField = null;
			Type currentType = current.GetType();
			while (currentType != typeof(object))
			{
				targetField = currentType!.GetField(fieldName, flags);
				if (targetField != null)
					break;

				currentType = currentType.BaseType;
			}

			return targetField;
		}
	}
}