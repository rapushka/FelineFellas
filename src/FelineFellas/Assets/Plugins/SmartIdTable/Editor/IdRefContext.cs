using System;
using UnityEditor;

namespace SmartIdTable.Editor
{
	public class IdRefContext
	{
		public IdRefContext(string[] options, SerializedProperty property, IdTable idTable, bool hasReferenceToTable)
		{
			Options = options;
			Property = property;
			IdTable = idTable;
			HasReferenceToTable = hasReferenceToTable;
		}
		
		public string[] Options { get; }
		public SerializedProperty Property { get; }
		public IdTable IdTable { get; }
		public bool HasReferenceToTable { get; }
	}
}