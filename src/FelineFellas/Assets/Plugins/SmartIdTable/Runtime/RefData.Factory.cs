#if UNITY_EDITOR
using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace SmartIdTable
{
	public partial class RefData
	{
		public static RefData FromAssetPath([NotNull] string assetPath, string propertyPath) => FromAsset(AssetDatabase.LoadAssetAtPath<Object>(assetPath), propertyPath);

		public static RefData FromAsset([NotNull] Object asset, string propertyPath)
		{
			if (!AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out string guid, out long localId))
				throw new ArgumentException($"{asset} isn't a persistent asset", nameof(asset));

			return new RefData(BuildObjectPath(guid, null, localId), propertyPath, RefType.ScriptableObject);
		}

		public static RefData Create([NotNull] Object obj, string propertyPath)
		{
			RefType refType = GetReferenceType(obj);
			string objectPath = GetObjectPath(obj, refType);
			return new RefData(objectPath, propertyPath, refType);
		}

		private static RefType GetReferenceType(Object obj)
		{
			switch (obj)
			{
				case ScriptableObject:
					return RefType.ScriptableObject;
				case Component component:
				{
					PrefabStage currentPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();

					if (PrefabUtility.IsPartOfPrefabAsset(component))
						return RefType.PrefabRootComponent;

					if (currentPrefabStage != null) // If we have prefab mode open
						return component.gameObject == currentPrefabStage.prefabContentsRoot ? RefType.PrefabRootComponent : RefType.PrefabChildComponent;

					return RefType.SceneObject;
				}
				default:
					throw new ArgumentException(nameof(obj));
			}
		}

		private static string GetObjectPath(Object obj, RefType refType)
		{
			string guid, scenePath = string.Empty;
			long localId;

			switch (refType)
			{
				case RefType.SceneObject:
				{
					// Disabled in Lite version
					guid = string.Empty;
					localId = default;
					break;
				}
				case RefType.ScriptableObject:
				{
					AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out guid, out localId);
					break;
				}
				case RefType.PrefabRootComponent:
				case RefType.PrefabChildComponent:
				{
					string assetPath = AssetDatabase.GetAssetPath(Utils.IsPartOfPrefabContents(obj, out Object prefabAsset) ? prefabAsset : obj);
					guid = AssetDatabase.AssetPathToGUID(assetPath);
					localId = Utils.GetLocalID(obj);
					break;
				}
				default:
					throw new ArgumentOutOfRangeException();
			}

			string objectPath = BuildObjectPath(guid, scenePath, localId);
			return objectPath;
		}

		private static string BuildObjectPath(string guid, string scenePath, long localId) => $"{guid}|{scenePath}|{localId}";
	}
}
#endif