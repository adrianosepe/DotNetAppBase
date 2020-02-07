using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public static class Parser
		{
			public static string ExtractContent(string content, string pattern)
			{
				return Regex
					.Split(content, pattern)
					.FirstOrDefault(s => s != String.Empty);
			}

			public static string ExtractNumber(string content)
			{
				var value = Regex
					.Split(content, @"\D+")
					.FirstOrDefault(s => s != String.Empty);

				return value;
			}

			public static IEnumerable<string> ExtractNumbers(string content)
			{
				return Regex
					.Split(content, @"\D+")
					.Where(s => s != String.Empty);
			}

			public static IEnumerable<string> ExtractWords(string content)
			{
				return Regex
					.Split(content, @"\s+")
					.Where(s => s != String.Empty);
			}
		}
	}
}