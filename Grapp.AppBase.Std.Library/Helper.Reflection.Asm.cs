using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public partial class Reflections
		{
			public static class Asm
			{
				public static TAttribute GetAttribute<TAttribute>(Assembly asm)
				{
					return asm.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>().FirstOrDefault();
				}

				public static IEnumerable<TAttribute> GetAttributes<TAttribute>(Assembly asm)
				{
					return asm.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
				}
			}
		}
	}
}