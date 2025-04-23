using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace SmartIdTable
{
	[Serializable]
	public class IdDefinition : IComparable<IdDefinition>
	{
		[SerializeField, FormerlySerializedAs("Id")]
		private string _id;
		[SerializeField] private List<RefData> _assetRefs = new();

		public IdDefinition() { }

		public IdDefinition(string id)
		{
			_id = id;
		}

		public string Id
		{
			get => _id;
#if UNITY_EDITOR
			set
			{
				this.UpdateAllIdReferences(value);
				_id = value;
			}
#endif
		}

		public IEnumerable<RefData> AssetRefs => _assetRefs;

		/// <summary>
		/// Adds a new RefData object to the AssetRefs collection.
		/// </summary>
		/// <param name="refData">Reference to an asset</param>
		/// <returns>True if the object was successfully added, false if it already exists in the collection.</returns>
		public bool AddObjectRef(RefData refData)
		{
			if (_assetRefs.Contains(refData))
				return false;

			_assetRefs.Add(refData);
			return true;
		}

		/// <summary>
		/// Removes the specified RefData object from the AssetRefs collection.
		/// </summary>
		/// <param name="refData">Reference to an object</param>
		/// <returns>True if the object was successfully removed, false otherwise.</returns>
		public bool RemoveObjectRef(RefData refData) => _assetRefs.Remove(refData);

		/// <summary>
		/// Removes all RefData objects with the specified GUID from the AssetRefs collection.
		/// </summary>
		/// <param name="guid">Target GUID</param>
		/// <returns>True if any objects were removed, false otherwise.</returns>
		public bool RemoveAllObjectRefs(string guid)
		{
			var toRemove = _assetRefs.Where(x => x.GUID == guid).ToList();
			_assetRefs = _assetRefs.Except(toRemove).ToList();
			
			return toRemove.Count > 0;
		}

		/// <summary>
		/// Checks if the specified RefData object exists in the AssetRefs collection.
		/// </summary>
		/// <param name="refData">RefData to check against</param>
		/// <returns>True if it exists, false otherwise.</returns>
		public bool Contains(RefData refData) => _assetRefs.Contains(refData);

		/// <summary>
		/// Compares this IdDefinition to another IdDefinition based on their ID values.
		/// </summary>
		/// <param name="other">Another <c>IdDefinition</c> object</param>
		/// <returns>Comparison result.</returns>
		public int CompareTo(IdDefinition other)
		{
			if (ReferenceEquals(this, other))
				return 0;
			if (ReferenceEquals(null, other))
				return 1;
			
			return Utils.CompareStringsPushEmptyDown(_id, other._id);
		}
	}
}