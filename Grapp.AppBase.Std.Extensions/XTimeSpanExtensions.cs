using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Primitives;

// ReSharper disable CheckNamespace
public static class XTimeSpanExtensions
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Days.
	/// </summary>
	public static XTimeSpan Days(this int days)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromDays(days)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Days.
	/// </summary>
	public static XTimeSpan Days(this double days)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromDays(days)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Hours.
	/// </summary>
	public static XTimeSpan Hours(this int hours)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromHours(hours)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Hours.
	/// </summary>
	public static XTimeSpan Hours(this double hours)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromHours(hours)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Milliseconds.
	/// </summary>
	public static XTimeSpan Milliseconds(this int milliseconds)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromMilliseconds(milliseconds)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Milliseconds.
	/// </summary>
	public static XTimeSpan Milliseconds(this double milliseconds)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromMilliseconds(milliseconds)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Minutes.
	/// </summary>
	public static XTimeSpan Minutes(this int minutes)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromMinutes(minutes)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Minutes.
	/// </summary>
	public static XTimeSpan Minutes(this double minutes)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromMinutes(minutes)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> value for given number of Months.
	/// </summary>
	public static XTimeSpan Months(this int months)
	{
		return new XTimeSpan {Months = months};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Seconds.
	/// </summary>
	public static XTimeSpan Seconds(this int seconds)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromSeconds(seconds)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Seconds.
	/// </summary>
	public static XTimeSpan Seconds(this double seconds)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromSeconds(seconds)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of ticks.
	/// </summary>
	public static XTimeSpan Ticks(this int ticks)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromTicks(ticks)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of ticks.
	/// </summary>
	public static XTimeSpan Ticks(this long ticks)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromTicks(ticks)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Weeks (number of weeks * 7).
	/// </summary>
	public static XTimeSpan Weeks(this int weeks)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromDays(value: weeks * 7)};
	}

	/// <summary>
	///     Returns <see cref="TimeSpan" /> for given number of Weeks (number of weeks * 7).
	/// </summary>
	public static XTimeSpan Weeks(this double weeks)
	{
		return new XTimeSpan {TimeSpan = TimeSpan.FromDays(value: weeks * 7)};
	}

	/// <summary>
	///     Generates <see cref="TimeSpan" /> value for given number of Years.
	/// </summary>
	public static XTimeSpan Years(this int years)
	{
		return new XTimeSpan {Years = years};
	}
}