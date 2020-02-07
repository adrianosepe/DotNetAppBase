using System;
using System.ComponentModel;
using System.Globalization;

namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public static partial class Data
		{
			[Localizable(false)]
			public static class Numeric
			{
				public static decimal Round(decimal value, int decimalPlaces)
				{
					var valorNovo = Decimal.Round(value, decimalPlaces);
					var valorNovoStr = valorNovo.ToString("F" + decimalPlaces, CultureInfo.CurrentCulture);

					return Decimal.Parse(valorNovoStr);
				}

				public static decimal? Round(decimal? value, int decimalPlaces)
				{
					if(value == null)
					{
						return null;
					}

					return Round(value.Value, decimalPlaces);
				}
			}
		}
	}
}