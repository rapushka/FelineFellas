using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SmartIdTable
{
	[Serializable]
	public partial class RefData : IEquatable<RefData>
	{
		internal const int CURRENT_API_VERSION = 1;

		public enum RefType
		{
			ScriptableObject = 0,
			PrefabRootComponent = 1,
			PrefabChildComponent = 2,
			SceneObject = 3,
		}

		[SerializeField, FormerlySerializedAs("ObjectPath")]
		private string _objectPath;

		[SerializeField, FormerlySerializedAs("TargetRefType")]
		private RefType _targetRefType;

		[SerializeField] private int _apiVersion;

		[SerializeField, FormerlySerializedAs("PropertyPath")]
		private string _propertyPath;

		public string PropertyPath => _propertyPath;

		public string GUID
		{
			get
			{
				ParsePath(out string guid, out _, out _);
				return guid;
			}
		}

		public RefType ReferenceType => _targetRefType;

#if UNITY_EDITOR
		private Object _targetObject;

		public Object EditorAsset
		{
			get
			{
				ParsePath(out string guid, out string _, out long _);
				return AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid));
			}
		}

#endif

		private RefData(string objectPath, string propertyPath, RefType refType)
		{
			_objectPath = objectPath;
			_apiVersion = CURRENT_API_VERSION;

			_propertyPath = propertyPath;
			_targetRefType = refType;
		}

		public static bool Equals([CanBeNull] RefData a, [CanBeNull] RefData b, RefComparison comparison = RefComparison.Default) => comparison switch
		{
			RefComparison.Default => string.Equals(a?._objectPath, b?._objectPath) && string.Equals(a?._propertyPath, b?._propertyPath),
			RefComparison.IgnorePropertyPath => string.Equals(a?._objectPath, b?._objectPath),
			_ => throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null)
		};

		public override bool Equals(object obj) => obj is RefData other && Equals(other);

		public bool Equals(RefData other)
		{
			if (other == null)
				return false;

			bool hasNoPath = _propertyPath == null || other._propertyPath == null;
			return Equals(this, other, hasNoPath ? RefComparison.IgnorePropertyPath : RefComparison.Default);
		}

		public override int GetHashCode() => HashCode.Combine(_objectPath, _propertyPath);

		public override string ToString() => $"{nameof(_objectPath)}: {_objectPath}, {nameof(_propertyPath)}: {_propertyPath}, {nameof(_targetRefType)}: {_targetRefType}";

		public void ParsePath(out string guid, out string scenePath, out long localId)
		{
			string[] splitPath = _objectPath.Split('|');
			guid = splitPath[0];
			scenePath = splitPath[1];
			localId = string.IsNullOrEmpty(splitPath[2]) ? 0 : long.Parse(splitPath[2]);
		}

		internal void UpdatePathFormat()
		{
			if (_apiVersion == CURRENT_API_VERSION)
				return;

			_objectPath = (_apiVersion, CURRENT_API_VERSION) switch
			{
				(0, 1) => $"{_objectPath}||",
				_ => throw new ArgumentOutOfRangeException()
			};

			_apiVersion = CURRENT_API_VERSION;
		}

#if UNITY_EDITOR

		public Object GetTargetObject()
		{
			if (_targetObject == null)
				_targetObject = LoadTargetObjectInternal();

			return _targetObject;
		}

		private Object LoadTargetObjectInternal()
		{
			ParsePath(out string guid, out string scenePath, out long localId);

			string assetPath = AssetDatabase.GUIDToAssetPath(guid);

			switch (_targetRefType)
			{
				case RefType.ScriptableObject:
					return AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
				case RefType.PrefabRootComponent:
				case RefType.PrefabChildComponent:
				{
					using SerializedObject serializedObject = Utils.GetSerializedObjectFromLocalID(assetPath, localId);
					return serializedObject?.targetObject;
				}
				case RefType.SceneObject:
					return null;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
#endif
	}
}