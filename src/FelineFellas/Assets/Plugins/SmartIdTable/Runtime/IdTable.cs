using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace SmartIdTable
{
	[CreateAssetMenu(order = -100, fileName = DEFAULT_NAME, menuName = DEFAULT_NAME)]
	public sealed class IdTable : ScriptableObject
	{
		public const string DEFAULT_NAME = "ID Table";
		private static readonly Dictionary<string, IdTable> TableCache = new();

		public static IdTable Get(string name = DEFAULT_NAME)
		{
			if (TableCache.TryGetValue(name, out IdTable table))
				return table;

			table = Resources.Load<IdTable>(name);
			if (table != null)
				TableCache.Add(name, table);

			return table;
		}

		[SerializeField] private List<string> _idList = new();
		[SerializeField] private List<IdDefinition> _idDefinitions = new();

		public IReadOnlyList<string> Ids => _idList;
		public IReadOnlyList<IdDefinition> IdDefinitions => _idDefinitions;

#if UNITY_EDITOR
		/// <summary>
		/// Adds new ID to the table
		/// </summary>
		/// <param name="id">ID to add. You can pass null to add empty ID</param>
		/// <param name="andSort">Whether the table should be sorted afterward. If you want to call the operation multiple times,
		/// set this to false and the call <see cref="OnIdTableUpdated"/> manually</param>
		/// <returns>False if the table already contains ID, true otherwise</returns>
		public bool Add(string id, bool andSort = true)
		{
			id ??= string.Empty;
			if (_idList.Contains(id))
				return false;

			Undo.RecordObject(this, "ID Table (Add ID)");

			_idList.Add(id);
			_idDefinitions.Add(new IdDefinition(id));

			if (andSort)
				OnIdTableUpdated();

			EditorUtility.SetDirty(this);
			return true;
		}

		/// <summary>
		/// Removes ID from the table
		/// </summary>
		/// <param name="id">ID to remove</param>
		/// <param name="andSort">Whether the table should be sorted afterward. If you want to call the operation multiple times,
		/// set this to false and the call <see cref="OnIdTableUpdated"/> manually</param>
		/// <returns>False if the table doesn't contain ID, true otherwise</returns>
		public bool Remove(string id, bool andSort = true)
		{
			if (!_idList.Contains(id))
				return false;

			Undo.RecordObject(this, "ID Table (Remove ID)");

			IdDefinition idDefinition = GetIdDefinition(id)!;
			foreach (RefData assetRef in idDefinition.AssetRefs)
			{
				Object editorAsset = assetRef.GetTargetObject();
				Debug.LogWarning($"{editorAsset} references ID '{id}' that got deleted", editorAsset);
			}

			_idList.Remove(id);
			_idDefinitions.Remove(idDefinition);

			if (andSort)
				OnIdTableUpdated();

			EditorUtility.SetDirty(this);
			return true;
		}

		public bool Contains(string id) => _idList.Contains(id);

		/// <summary>
		/// Replaces an ID in the table
		/// </summary>
		/// <param name="oldId">ID to replace</param>
		/// <param name="newId">ID to replace with</param>
		/// <param name="andSort">Whether the table should be sorted afterward. If you want to call the operation multiple times,
		/// set this to false and the call <see cref="OnIdTableUpdated"/> manually</param>
		/// <returns>False if the table doesn't contain <paramref name="oldId"/>, true otherwise</returns>
		public bool Replace(string oldId, string newId, bool andSort = true)
		{
			int indexOf = _idList.IndexOf(oldId);
			if (indexOf == -1)
				return false;

			Undo.RecordObject(this, "ID Table (Replace ID)");
			_idList[indexOf] = newId;

			IdDefinition idDefinition = GetIdDefinition(oldId)!;
			idDefinition.Id = newId;

			if (andSort)
				OnIdTableUpdated();

			EditorUtility.SetDirty(this);
			return true;
		}

		/// <summary>
		/// Creates a connection between an ID and the object that uses it
		/// </summary>
		/// <param name="id">Target ID</param>
		/// <param name="refData">Reference to the target object</param>
		/// <returns>False if the table doesn't contain ID, true otherwise</returns>
		public bool AddReferenceToId(string id, RefData refData)
		{
			IdDefinition idDefinition = GetIdDefinition(id);
			if (idDefinition == null)
				return false;

			idDefinition.AddObjectRef(refData);
			EditorUtility.SetDirty(this);
			return true;
		}

		/// <summary>
		/// Removes the connection between an ID and the object that uses it
		/// </summary>
		/// <param name="id">Target ID</param>
		/// <param name="refData">Reference to the target object</param>
		/// <returns>False if the table doesn't contain ID, true otherwise</returns>
		public bool RemoveReferenceFromId(string id, RefData refData)
		{
			IdDefinition idDefinition = GetIdDefinition(id);
			if (idDefinition == null)
				return false;

			idDefinition.RemoveObjectRef(refData);
			EditorUtility.SetDirty(this);
			return true;
		}
#endif

		/// <summary>
		/// Removes all references of the target object from the table
		/// </summary>
		/// <param name="refData">Reference to the target object</param>
		/// <returns>False if the object doesn't use any IDs from the table</returns>
		public bool RemoveAllReferencesOfObject(RefData refData)
		{
			return _idDefinitions.Aggregate(false, (current, idDefinition) => current | idDefinition.RemoveObjectRef(refData));
		}

		/// <summary>
		/// Removes all references of the target asset from the table
		/// </summary>
		/// <param name="guid">GUID of the target asset</param>
		/// <returns>False if the asset doesn't use any IDs from the table</returns>
		public bool RemoveAllReferencesOfObject(string guid)
		{
			return _idDefinitions.Aggregate(false, (current, idDefinition) => current | idDefinition.RemoveAllObjectRefs(guid));
		}

		/// <summary>
		/// Sorts the table entries, with empty ones pushed to the top.
		/// </summary>
		public void OnIdTableUpdated()
		{
			_idList.Sort(Utils.CompareStringsPushEmptyDown);
			_idDefinitions.Sort();
		}

		[CanBeNull]
		private IdDefinition GetIdDefinition(string id)
		{
			return _idDefinitions.FirstOrDefault(x => x.Id == id);
		}
	}
}