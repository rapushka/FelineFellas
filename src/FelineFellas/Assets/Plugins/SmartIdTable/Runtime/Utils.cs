using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace SmartIdTable
{
	public static class Utils
	{
#if UNITY_EDITOR

#if ID_TABLE_DEBUG
		[MenuItem("ID Table/Debug/Update Asset Ref Paths")]
#endif
		private static void UpdateAssetRefPaths()
		{
			foreach (IdTable idTable in GetAllIdTables())
			{
				foreach (IdDefinition idDefinition in idTable.IdDefinitions)
				foreach (RefData assetRef in idDefinition.AssetRefs)
				{
					assetRef.UpdatePathFormat();
				}

				EditorUtility.SetDirty(idTable);
			}
		}
		
		public static IdTable[] GetAllIdTables() => Resources.LoadAll<IdTable>(string.Empty);
		
		internal static void UpdateAllIdReferences(this IdDefinition idDefinition, string newId)
		{
			foreach (RefData refData in idDefinition.AssetRefs)
			{
				if (refData.ReferenceType is RefData.RefType.SceneObject)
				{
					// Disabled in Lite version
				}
				else
				{
					UpdateFieldValue(refData.GetTargetObject(), refData.PropertyPath, newId);
				}
			}
		}

		public static void UpdateFieldValue(Object targetObject, string propertyPath, string newId)
		{
			using var serObj = new SerializedObject(targetObject);
			SerializedProperty property = serObj.FindProperty(propertyPath);
			property.stringValue = newId;
			serObj.ApplyModifiedProperties();
		}

		public static bool TryGetSceneIfLoaded(string scenePath, out Scene scene)
		{
			scene = SceneManager.GetSceneByPath(scenePath);
			return scene.IsValid();
		}

		public static string ConvertGlobalPathToLocal(string path)
		{
			const char separator = '/';
			string[] splitPath = path.Split(separator);
			return splitPath[^1];
		}

		public static void AddRange(this IdTable idTable, IEnumerable<string> ids)
		{
			foreach (string id in ids)
			{
				idTable.Add(id, andSort: false);
			}

			idTable.OnIdTableUpdated();
		}

		public static void RemoveThisAndChildIds(this IdTable idTable, string id)
		{
			var cleanupList = idTable.Ids.Where(x => x.StartsWith(id)).ToList();

			foreach (string idToRemove in cleanupList)
			{
				idTable.Remove(idToRemove, andSort: false);
			}

			idTable.OnIdTableUpdated();
		}

		private const string LOCAL_ID_PROPERTY_PATH = "m_LocalIdentfierInFile"; // This is NOT a typo

		public static SerializedObject GetSerializedObjectFromLocalID(string assetPath, long localID)
		{
			foreach (MonoBehaviour behaviour in AssetDatabase.LoadAllAssetsAtPath(assetPath).OfType<MonoBehaviour>())
			{
				SerializedObject serializedObject = GetDebugModeSerializedObject(behaviour);

				SerializedProperty localIdProperty = serializedObject.FindProperty(LOCAL_ID_PROPERTY_PATH);

				if (localIdProperty.longValue == localID)
					return serializedObject;

				serializedObject.Dispose();
			}

			return null;
		}

		public static long GetLocalID(Object obj)
		{
			using SerializedObject serializedObject = GetDebugModeSerializedObject(obj);
			SerializedProperty localIdProperty = serializedObject.FindProperty(LOCAL_ID_PROPERTY_PATH);
			return localIdProperty.longValue;
		}

		private static readonly PropertyInfo InspectorModeInfo = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);

		private static SerializedObject GetDebugModeSerializedObject(Object target)
		{
			var serializedObject = new SerializedObject(target);
			InspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug);
			return serializedObject;
		}

		public static bool IsPartOfPrefabContents(Object obj, out Object prefabAsset)
		{
			if (obj is not Component component)
			{
				prefabAsset = null;
				return false;
			}

			PrefabStage currentPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();
			if (currentPrefabStage == null)
			{
				prefabAsset = null;
				return false;
			}

			prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(currentPrefabStage.assetPath);
			return component.transform.root.gameObject == currentPrefabStage.prefabContentsRoot;
		}
#endif

		public static int CompareStringsPushEmptyDown(string s1, string s2)
		{
			bool s1Empty = string.IsNullOrEmpty(ConvertGlobalPathToLocal(s1));
			bool s2Empty = string.IsNullOrEmpty(ConvertGlobalPathToLocal(s2));

			if (s1Empty && !s2Empty)
				return 1;
			if (s2Empty && !s1Empty)
				return -1;

			return string.Compare(s1, s2, StringComparison.Ordinal);
		}
	}
}