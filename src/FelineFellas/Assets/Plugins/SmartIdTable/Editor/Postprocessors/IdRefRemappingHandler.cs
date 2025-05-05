using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using Debug = UnityEngine.Debug;
using RuntimeUtils = SmartIdTable.Utils;

namespace SmartIdTable.Editor.Postprocessors
{
	internal static class IdRefRemappingHandler
	{
		[DidReloadScripts]
		private static void OnCompilationFinished()
		{
			Log("Compilation finished");
			ProcessScriptReimport(out RemappingData remappingData);

			bool updatedAnyTable = false;
			foreach (IdTable idTable in RuntimeUtils.GetAllIdTables())
			{
				if (!remappingData.TryGetValue(idTable.name, out Dictionary<string, List<RefData>> remaps))
					continue;

				bool didRemaps = ProcessRemaps(idTable, remaps);

				if (didRemaps)
					continue;

				updatedAnyTable = true;
				EditorUtility.SetDirty(idTable);
			}

			if (updatedAnyTable)
			{
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
		}

		private static bool ProcessRemaps(IdTable idTable, Dictionary<string, List<RefData>> remaps)
		{
			bool setDirty = false;

			foreach ((string id, List<RefData> refList) in remaps)
			{
				if (!idTable.Contains(id))
					continue;

				foreach (RefData refData in refList)
				{
					idTable.AddReferenceToId(id, refData);
					setDirty = true;
				}
			}

			return setDirty;
		}

		private class RemappingData : Dictionary<string, Dictionary<string, List<RefData>>> { }

		private static void ProcessScriptReimport(out RemappingData remappingData)
		{
			// foreach IDTable
			// Проходимся по всем рефам в таблице - Done
			// Проходимся по PropertyPath, берём атрибут - Done
			// Смотрим, сходится ли название таблицы с названием из атрибута - Done
			// Если нет, то добавляем в список на перенос - Done
			// Наверное удаляем запись сразу же - Done

			// Нужно перенести в такую-то таблицу список референсов на такой-то ID

			remappingData = new RemappingData();

			foreach (IdTable idTable in RuntimeUtils.GetAllIdTables())
			foreach (IdDefinition idDefinition in idTable.IdDefinitions)
			{
				var refsToRemove = new List<RefData>();

				foreach (RefData assetRef in idDefinition.AssetRefs)
				{
					if (assetRef.ReferenceType is RefData.RefType.SceneObject)
						continue;
					
					var fieldInfo = ReflectionUtils.AssetRefToFieldInfo(assetRef, out object fieldHolder);

					if (fieldInfo is null)
						continue;

					var idRefAttribute = fieldInfo.GetCustomAttribute<IdRefAttribute>();
					string currentValue = (string)fieldInfo.GetValue(fieldHolder);

					string desiredTableName = idRefAttribute.TableName;
					if (Utils.IsSpecialValue(currentValue) || desiredTableName == idTable.name)
						continue;

					Log($"Need to move asset ref {assetRef.PropertyPath} from table '{idTable.name}' to '{desiredTableName}'");
					if (!remappingData.TryGetValue(desiredTableName, out Dictionary<string, List<RefData>> tableRemaps))
					{
						tableRemaps = new Dictionary<string, List<RefData>>();
						remappingData.Add(desiredTableName, tableRemaps);
					}

					if (!tableRemaps.TryGetValue(currentValue, out List<RefData> refList))
					{
						refList = new List<RefData>();
						tableRemaps.Add(currentValue, refList);
					}

					refList.Add(assetRef);
					refsToRemove.Add(assetRef);
				}

				foreach (RefData refData in refsToRemove)
				{
					idDefinition.RemoveObjectRef(refData);
				}

				if (refsToRemove.Count > 0)
					EditorUtility.SetDirty(idTable);
			}
		}
		
		[Conditional("ID_TABLE_ENABLE_LOGS")]
		private static void Log(object message) => Debug.Log(message);
	}
}