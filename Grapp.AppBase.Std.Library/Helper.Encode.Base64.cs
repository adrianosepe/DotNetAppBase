using System;

namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public partial class Encodes
		{
			public static class Base64
			{
				public static byte[] FromBase64String(string str)
				{
					return Convert.FromBase64String(str);
				}

				public static string ToBase64String(byte[] bytes)
				{
					return Convert.ToBase64String(bytes);
				}
			}
		}
	}
}