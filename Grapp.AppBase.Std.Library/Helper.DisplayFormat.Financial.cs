using System.ComponentModel;
using System.Globalization;

namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public static partial class DisplayFormat
		{
			public static class Financial
			{
				public static string CurrencySymbol => CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;

				[Localizable(false)]
				public static string AsCurrency(decimal value)
				{
					return value.ToString("C");
				}

				[Localizable(false)]
				public static string Format(decimal value)
				{
					return value.ToString("F2");
				}
			}
		}
	}
}