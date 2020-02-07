using System;
using System.ComponentModel;
using System.Text;
using Grapp.AppBase.Std.Extensions.Properties;

// ReSharper disable CheckNamespace
public static class XTimeSpanNativeExtensions
// ReSharper restore CheckNamespace
{
	public static TimeSpan InDays(this int value)
	{
		return TimeSpan.FromDays(value);
	}

	public static TimeSpan InDays(this double value)
	{
		return TimeSpan.FromDays(value);
	}

	public static TimeSpan InHours(this int value)
	{
		return TimeSpan.FromHours(value);
	}

	public static TimeSpan InHours(this double value)
	{
		return TimeSpan.FromHours(value);
	}

	public static TimeSpan InMinutes(this int value)
	{
		return TimeSpan.FromMinutes(value);
	}

	public static TimeSpan InMinutes(this double value)
	{
		return TimeSpan.FromMinutes(value);
	}

	public static TimeSpan InSeconds(this int value)
	{
		return TimeSpan.FromSeconds(value);
	}

	public static TimeSpan InSeconds(this double value)
	{
		return TimeSpan.FromSeconds(value);
	}

	public static string ToDefaultDisplay(this TimeSpan? span)
	{
		return span == null ? DbMessages.XTimeSpanNativeExtensions_FailOnCalculate : ToDefaultDisplay(span.Value);
	}

	[Localizable(isLocalizable: false)]
	public static string ToDefaultDisplay(this TimeSpan span)
	{
		var builder = new StringBuilder();

		double fraction;
		if(span.Days > 0)
		{
			builder.Append(
				value: $"{span.Days} dia{(span.Days > 1 ? "s" : String.Empty)} ");

			fraction = span.TotalHours - span.Days * 24;

			builder.Append(
				value: $"{fraction:f2} hora{(fraction > 1 ? "s" : String.Empty)}");
		}
		else
		{
			if(span.Hours > 0)
			{
				builder.Append(
					value: $"{span.Hours} hora{(span.Hours > 1 ? "s" : String.Empty)} ");
			}

			if(span.TotalMinutes > 1)
			{
				fraction = span.TotalMinutes - span.Hours * 60;

				builder.Append(
					value: $"{fraction:f1} minuto{(fraction > 1 ? "s" : String.Empty)}");
			}
			else
			{
				builder.Append(value: "Menos de 1 minuto.");
			}
		}

		return builder.ToString();
	}
}