using System;
using System.Data;
using System.Linq;

// ReSharper disable CheckNamespace

public static class XDataRecordExtensions
{
	public static object GetValue(this IDataRecord record, string name)
	{
		var ordinal = record.GetOrdinal(name);
		if(record.IsDBNull(ordinal))
		{
			return null;
		}

		return record.GetValue(ordinal);
	}
}