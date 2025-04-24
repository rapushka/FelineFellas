using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SmartIdTable.Editor
{
	public class IdRefSearchWindow : ScriptableObject, ISearchWindowProvider
	{
		private static readonly StringBuilder STRING_BUILDER = new();

		private IdRefContext _context;
		private Action<IdRefContext,string> _callback;

		public static IdRefSearchWindow Create(IdRefContext context, Action<IdRefContext, string> callback)
		{
			var searchWindow = CreateInstance<IdRefSearchWindow>();
			searchWindow._context = context;
			searchWindow._callback = callback;

			return searchWindow;
		}

		public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
		{
			var searchTreeEntries = new List<SearchTreeEntry>
			{
				new SearchTreeGroupEntry(new GUIContent("ID List"))
			};

			searchTreeEntries.AddRange(ProcessIdTable());
			return searchTreeEntries;
		}

		public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
		{
			_callback(_context, (string)searchTreeEntry.userData);
			return true;
		}

		private IEnumerable<SearchTreeEntry> ProcessIdTable()
		{
			List<string> groups = new();

			foreach (string id in _context.Options)
			{
				string[] entryTitle = id.Split(Utils.SEPARATOR);

				STRING_BUILDER.Clear();

				for (int i = 0; i < entryTitle.Length - 1; i++)
				{
					string value = entryTitle[i];
					STRING_BUILDER.Append(value);

					string groupName = STRING_BUILDER.ToString();
					if (!groups.Contains(groupName))
					{
						yield return new SearchTreeGroupEntry(new GUIContent(value), i + 1);
						groups.Add(groupName);
					}

					STRING_BUILDER.Append(Utils.SEPARATOR);
				}

				string entryNameLastSegment = IdRefDrawer.ParseSpecialValue(entryTitle[^1]);
				yield return new SearchTreeEntry(new GUIContent(entryNameLastSegment))
				{
					level = entryTitle.Length,
					userData = id
				};
			}
		}
	}
}