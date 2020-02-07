using System;
using System.ComponentModel;

namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public static class Bind
		{
			public static string GetDefaultBindingProperty(Type type)
			{
				return Reflections.Attributes.Get<DefaultBindingPropertyAttribute>(type)?.Name;
			}

			public static DefaultBindingPropertyAttribute GetDefaultBindingPropertyAttribute(Type type)
			{
				return Reflections.Attributes.Get<DefaultBindingPropertyAttribute>(type);
			}
		}
	}
}