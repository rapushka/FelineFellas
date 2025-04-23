using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace SmartIdTable
{
	[AttributeUsage(AttributeTargets.Field)]
	public class IdRefAttribute : PropertyAttribute
	{
		/// <param name="startsWith">Options can be separated with '|'</param>
		/// <param name="endsWith">Options can be separated with '|'</param>
		/// <param name="tableName"></param>
		public IdRefAttribute(string startsWith = null, string endsWith = null, [NotNull] string tableName = IdTable.DEFAULT_NAME)
		{
			TableName = tableName;
			StartsWith = startsWith?.Split('|') ?? Array.Empty<string>();
			EndsWith = endsWith?.Split('|') ?? Array.Empty<string>();
		}

		public string[] StartsWith { get; }
		public string[] EndsWith { get; }	
		public string TableName { get; }

	}
}