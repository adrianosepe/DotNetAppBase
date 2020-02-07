using System;
using System.Globalization;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Contract;

// ReSharper disable CheckNamespace
public static partial class XObjectExtensions
// ReSharper restore CheckNamespace
{
	private const string NullDefaultDisplay = "<<NULL>>";

	public static string XDisplayAs(this DateTime dateTime, EDisplayAs displayAs)
	{
		switch(displayAs)
		{
			case EDisplayAs.Date:
				return dateTime.ToString(format: "d", CultureInfo.CurrentCulture);

			case EDisplayAs.Time:
				return dateTime.ToString(format: "t", CultureInfo.CurrentCulture);

			case EDisplayAs.DateTime:
				return dateTime.ToString(format: "G", CultureInfo.CurrentCulture);

			default:
				throw XArgumentOutOfRangeException.Create(paramName: nameof(displayAs), displayAs, message: null);
		}
	}
}