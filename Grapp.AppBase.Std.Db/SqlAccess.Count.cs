using System;

namespace Grapp.ApplicationBase.Db
{
	public partial class SqlAccess
	{
		public struct Count
		{
			public Count(int value)
			{
				Value = value;
			}

			public int Value { get; set; }

			public static implicit operator Count(int count)
			{
				return new Count(count);
			}

			public static implicit operator int(Count count)
			{
				return count.Value;
			}

			public static bool operator &(Count count, int value)
			{
				return count.Value == value;
			}
		}
	}
}