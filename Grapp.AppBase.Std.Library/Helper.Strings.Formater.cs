using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public static partial class Strings
		{
			public static class Formater
			{
				public static string Interpolation(string original, object obj)
				{
					const string openToken = "{";
					const string closeToken = "}";

					var properties = TypeDescriptor
						.GetProperties(obj)
						.Cast<PropertyDescriptor>()
						.ToArray();

					var builder = new StringBuilder(original);

					Enumerable.ForEach(
						RegEx.Matches(openToken, closeToken, original),
						info =>
							{
								var prop = properties.FirstOrDefault(descriptor => descriptor.Name == info.Content);
								if(prop == null)
								{
									return;
								}

								builder.Replace(info.Expression, prop.GetValue(obj)?.ToString() ?? String.Empty);
							});

					return builder.ToString();
				}

				public static string SeparatedStringToCamelCase(string str)
				{
					return I18n.CurrentCulture.TextInfo.ToTitleCase(str);
				}

				public static string SepareteCamelCase(string str)
				{
					return Regex.Replace(str, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
				}
			}
		}
	}
}