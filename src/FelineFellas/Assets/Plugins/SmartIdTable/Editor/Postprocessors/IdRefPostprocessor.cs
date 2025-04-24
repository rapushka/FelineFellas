using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace SmartIdTable.Editor.Postprocessors
{
	internal class IdRefPostprocessor : AssetPostprocessor
	{
		private readonly struct AssetRefData
		{
			public AssetRefData(Object asset, FieldInfo[] fieldsWithAttribute)
			{
				Asset = asset;
				FieldsWithAttribute = fieldsWithAttribute;
			}

			public Object Asset { get; }
			public FieldInfo[] FieldsWithAttribute { get; }
		}

		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			bool updatedAnyTable = false;

			List<Object> importedAssetsLoaded = LoadAssetsFromPaths(importedAssets).ToList();

			foreach (IdTable idTable in SmartIdTable.Utils.GetAllIdTables().AsParallel())
			{
				bool updatedTable = false;

				updatedTable |= ProcessDeletedAssets(idTable, deletedAssets);
				updatedTable |= ProcessImportedAssets(idTable, importedAssetsLoaded);

				if (updatedTable)
				{
					updatedAnyTable = true;
					EditorUtility.SetDirty(idTable);
				}
			}

			if (updatedAnyTable)
			{
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
		}

		private static bool ProcessImportedAssets(IdTable table, ICollection<Object> importedAssets)
		{
			bool madeChanges = false;
			foreach (GameObject prefab in importedAssets.OfType<GameObject>())
			foreach (IdDefinition idDefinition in table.IdDefinitions.AsParallel())
			{
				var invalidRefs = idDefinition.AssetRefs.Where(x => x.EditorAsset == prefab && x.GetTargetObject() == null).ToList();
				foreach (RefData invalidRef in invalidRefs)
				{
					idDefinition.RemoveObjectRef(invalidRef);
				}

				if (invalidRefs.Count > 0)
					madeChanges = true;
			}

			foreach (AssetRefData data in GetAllFieldsWithIdRefAttribute(importedAssets))
			{
				foreach ((string id, string fieldName) in GetFieldsThatUseIdTable(table, data))
				{
					RefData refData = RefData.FromAsset(data.Asset, fieldName);

					table.AddReferenceToId(id, refData);
					madeChanges = true;
				}
			}

			return madeChanges;
		}

		private static IEnumerable<AssetRefData> GetAllFieldsWithIdRefAttribute(IEnumerable<Object> assets)
		{
			return
				from asset in assets
				let applicableFields = GetApplicableFields(asset.GetType()).Where(FieldHasItemIdAttribute).ToArray()
				where applicableFields.Length > 0
				select new AssetRefData(asset, applicableFields);

			static bool FieldHasItemIdAttribute(FieldInfo fieldInfo) => fieldInfo.CustomAttributes.Any(x => x.AttributeType == typeof(IdRefAttribute));

			static FieldInfo[] GetApplicableFields(Type type)
			{
				const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
				return type.GetFields(flags);
			}
		}

		private static IEnumerable<(string id, string fieldName)> GetFieldsThatUseIdTable(IdTable table, AssetRefData data)
		{
			foreach (FieldInfo field in data.FieldsWithAttribute)
			{
				if (field.GetValue(data.Asset) is string id && table.Ids.Contains(id))
					yield return (id, field.Name);
			}
		}

		private static bool ProcessDeletedAssets(IdTable table, IEnumerable<string> deletedAssets)
		{
			int itemsUnregistered = deletedAssets.Select(AssetDatabase.AssetPathToGUID).Count(AssetWasUnregistered);
			if (itemsUnregistered > 0)
				Log($"Total objects unregistered: {itemsUnregistered}");

			return itemsUnregistered > 0;

			bool AssetWasUnregistered(string guid)
			{
				if (table.RemoveAllReferencesOfObject(guid))
				{
					// Log($"Removed object {refData.ObjectPath} from the ref table");
					return true;
				}

				return false;
			}
		}

		private static IEnumerable<Object> LoadAssetsFromPaths(IEnumerable<string> paths) => paths.Select(AssetDatabase.LoadAssetAtPath<Object>).Where(x => x != null);

		[Conditional("ID_TABLE_ENABLE_LOGS")]
		private static void Log(object message) => Debug.Log(message);
	}
}