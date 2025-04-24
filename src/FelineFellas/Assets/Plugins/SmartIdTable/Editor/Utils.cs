using System.Collections.Generic;
using System.Linq;
using SmartIdTable.Editor.View;
using UnityEditor;
using UnityEngine;
using static SmartIdTable.Utils;

namespace SmartIdTable.Editor
{
	public static class Utils
	{
		public const char SEPARATOR = '/';

		public static void DFSUtil(IdDefinitionView view, Queue<IdDefinitionView> queue)
		{
			foreach (IdDefinitionView childView in view.Children().OfType<IdDefinitionView>())
			{
				DFSUtil(childView, queue);
			}

			queue.Enqueue(view);
		}

		public static string UpdateGlobalPath(string path, string newLocal)
		{
			string[] splitPath = path.Split(SEPARATOR);
			splitPath[^1] = newLocal;

			return string.Join(SEPARATOR, splitPath);
		}

		public static string UpdateGlobalPath(string path, int depth, string updatedSegment)
		{
			string[] splitPath = path.Split(SEPARATOR);
			splitPath[depth] = updatedSegment;

			return string.Join(SEPARATOR, splitPath);
		}

		public static bool ContainsReferenceTo(this IdTable table, SerializedProperty property)
		{
			if (property.propertyType is not SerializedPropertyType.String)
				return false;

			IdDefinition idDefinition = table.IdDefinitions.FirstOrDefault(x => x.Id == property.stringValue);
			if (idDefinition == null)
				return false;

			Object targetObject = property.serializedObject.targetObject;
			bool isPartOfPrefabContents = IsPartOfPrefabContents(targetObject, out Object prefabAsset);

			return idDefinition.AssetRefs.Any(assetRef =>
			{
				if (assetRef.PropertyPath != property.propertyPath)
					return false;

				if (isPartOfPrefabContents)
				{
					assetRef.ParsePath(out _, out _, out long localId);
					return prefabAsset == assetRef.EditorAsset && GetLocalID(targetObject) == localId;
				}

				return assetRef.GetTargetObject() == targetObject;
			});
		}

		public static int GetIdPathLength(string path) => path.Split(SEPARATOR).Length;
		public static bool IsSpecialValue(string currentValue) => currentValue.StartsWith(SpecialIdValues.SPECIAL_SYMBOL);
	}
}