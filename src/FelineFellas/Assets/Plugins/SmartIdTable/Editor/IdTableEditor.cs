using System.Collections.Generic;
using System.Linq;
using SmartIdTable.Editor.View;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SmartIdTable.Editor
{
	[CustomEditor(typeof(IdTable))]
	public class IdTableEditor : UnityEditor.Editor
	{
		[SerializeField] private VisualTreeAsset _visualTreeAsset;

		private ScrollView _scrollView;
		private Dictionary<string, IdDefinitionView> _viewMap;
		private IdTable _idTable;

		private void OnEnable()
		{
			_idTable = target as IdTable;
		}

		public override VisualElement CreateInspectorGUI()
		{
			VisualElement tree = _visualTreeAsset.CloneTree();
			tree.Bind(serializedObject);

			tree.Q<Button>("btn__add").clicked += () => AddNewId();
			_scrollView = tree.Q<ScrollView>();

			BuildIdTree();

			return tree;
		}

		private void AddNewId(string id = null)
		{
			if (_idTable.Add(id))
				BuildIdTree();
		}

		private void BuildIdTree()
		{
			foreach (IdDefinitionView view in _scrollView.Children().OfType<IdDefinitionView>().ToList())
			{
				OnRemoveIdDefinitionView(view);
				_scrollView.Remove(view);
			}

			foreach (IdDefinitionView view in GetRootIdViews(_idTable, out _viewMap))
			{
				// Debug.Log($"Creating with id {view.value}");

				_scrollView.Add(view);
			}

			OnItemCountUpdated();
		}

		private void OnItemCountUpdated()
		{
			const string className = "empty";
			if (_viewMap.Count == 0)
				_scrollView.AddToClassList(className);
			else
				_scrollView.RemoveFromClassList(className);
		}

		private void AddChildIdTo(IdDefinitionView view) => AddChildIdTo(view, string.Empty);

		private void AddChildIdTo(IdDefinitionView view, string localId)
		{
			if (string.IsNullOrEmpty(view.value))
			{
				Debug.LogWarning("Can't add child ID to an empty parent");
				return;
			}

			AddNewId($"{view.GlobalId}/{localId}");
		}

		private void RemoveId(IdDefinitionView view)
		{
			if (!_idTable.Contains(view.GlobalId))
				return;

			var queue = new Queue<IdDefinitionView>();
			Utils.DFSUtil(view, queue);

			while (queue.TryDequeue(out IdDefinitionView toRemove))
			{
				OnRemoveIdDefinitionView(toRemove);

				toRemove.RemoveFromHierarchy();
				_idTable.Remove(toRemove.GlobalId, andSort: false);
			}

			_idTable.OnIdTableUpdated();
			OnItemCountUpdated();
		}

		private void OnIdFieldUpdated(ChangeEvent<string> evt, IdDefinitionView view)
		{
			string updatedSegment = evt.newValue;
			bool inputIsComposite = updatedSegment.Contains(Utils.SEPARATOR);
			string[] splitId = null;

			if (inputIsComposite)
			{
				splitId = updatedSegment.Split(Utils.SEPARATOR);
				updatedSegment = splitId[0];
			}

			string newId = Utils.UpdateGlobalPath(view.GlobalId, updatedSegment);
			_idTable.Replace(view.GlobalId, newId);

			view.value = updatedSegment;
			evt.StopPropagation();

			if (view.childCount != 0) // No child paths need to be updated
			{
				int depth = Utils.GetIdPathLength(newId) - 1;
				foreach (IdDefinitionView childView in GetChildViews(view))
				{
					string newChildId = Utils.UpdateGlobalPath(childView.GlobalId, depth, updatedSegment);
					_idTable.Replace(childView.GlobalId, newChildId);
					childView.GlobalId = newChildId;
				}
			}

			if (inputIsComposite)
			{
				IdDefinitionView current = view;
				for (int i = 1; i < splitId.Length; i++)
				{
					string id = $"{current.GlobalId}/{splitId[i]}";
					_idTable.Add(id, andSort: false);

					var child = CreateIdDefinitionView(id);
					current.Add(child);

					current = child;
				}

				_idTable.OnIdTableUpdated();
			}

			return;

			static List<IdDefinitionView> GetChildViews(IdDefinitionView view)
			{
				var list = new List<IdDefinitionView>();
				GetChildViewsRecursive(view, list);

				return list;
			}

			static void GetChildViewsRecursive(IdDefinitionView view, List<IdDefinitionView> list)
			{
				foreach (IdDefinitionView childView in view.Children().Cast<IdDefinitionView>())
				{
					list.Add(childView);
					GetChildViewsRecursive(childView, list);
				}
			}
		}

		private List<IdDefinitionView> GetRootIdViews(IdTable idTable, out Dictionary<string, IdDefinitionView> viewMap)
		{
			viewMap = new Dictionary<string, IdDefinitionView>();
			var output = new List<IdDefinitionView>();

			foreach (string id in idTable.Ids)
			{
				const char separator = Utils.SEPARATOR;
				string[] splitPath = id.Split(separator);
				// Debug.Log($"Path is [{string.Join(", ", splitPath)}]");
				string parentPath = string.Join(separator, splitPath[..^1]);

				IdDefinitionView view = CreateIdDefinitionView(id);
				viewMap.Add(id, view);

				if (string.IsNullOrEmpty(parentPath))
				{
					output.Add(view);
				}
				else
				{
					IdDefinitionView parentView = viewMap[parentPath];
					parentView.Add(view);
				}
			}

			return output;
		}

		private IdDefinitionView CreateIdDefinitionView(string id)
		{
			var view = new IdDefinitionView(id);
			view.RegisterCallback<ChangeEvent<string>, IdDefinitionView>(OnIdFieldUpdated, view);
			view.AddButtonClicked += AddChildIdTo;
			view.RemoveButtonClicked += RemoveId;

			return view;
		}

		private void OnRemoveIdDefinitionView(IdDefinitionView view)
		{
			view.UnregisterCallback<ChangeEvent<string>, IdDefinitionView>(OnIdFieldUpdated);
			view.AddButtonClicked -= AddChildIdTo;
			view.RemoveButtonClicked -= RemoveId;
			_viewMap.Remove(view.GlobalId);
		}
	}
}