using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Grapp.AppBase.Std.Library;

// ReSharper disable CheckNamespace
[Localizable(isLocalizable: false)]
public static class XStringExtensions
// ReSharper restore CheckNamespace
{
    public static readonly Encoding DefaultEncoding = Encoding.UTF8;

	public static string AddOnLeft(this string value, string data)
	{
		return data + value;
	}

	public static string AddOnRight(this string value, string data)
	{
		return value + data;
	}

	// #region Convert
	public static byte AsByte(this string value)
	{
		return Convert.ToByte(value);
	}

	public static byte[] AsByteArray(this string value, Encoding encoding = null)
	{
		return (encoding ?? DefaultEncoding).GetBytes(value);
	}

	public static double AsDouble(this string value)
	{
		return Convert.ToDouble(value);
	}

	public static float AsFloat(this string value)
	{
		return Convert.ToSingle(value);
	}

	public static int AsInt32(this string value)
	{
		return Convert.ToInt32(value);
	}

	public static long AsInt64(this string value)
	{
		return Convert.ToInt64(value);
	}

	public static short AsShort(this string value)
	{
		return Convert.ToInt16(value);
	}

	public static string AsString(this byte[] data, Encoding encoding = null)
	{
        return (encoding ?? DefaultEncoding).GetString(data);
	}

	public static string BeginWith(this string value, string begin)
	{
		return value + begin;
	}

	public static string Concat(this IEnumerable<string> data, string separator = ", ")
	{
		var asArray = data.ToArrayEfficient();

		if(asArray.Length == 0)
		{
			return String.Empty;
		}

		return asArray.Aggregate((s, s1) => s + separator + s1);
	}

	public static string Delete(this string content, int countFromIndexZero, int countFromLastIndex = 0)
	{
		return content.Substring(countFromIndexZero, length: content.Length - (countFromLastIndex + 1));
	}

	public static string Insert(this string original, string insertValue, int offset)
	{
		var ivLength = insertValue.Length;

		var index = offset;
		while(index < original.Length)
		{
			original = original.Insert(index, insertValue);
			index += offset + ivLength;
		}

		return original;
	}

	public static bool IsNullOrEmpty(this string value)
	{
		return String.IsNullOrEmpty(value);
	}

	public static bool IsEmptyOrWhiteSpace(this string value)
	{
		return value.IsNullOrEmpty() || value.All(Char.IsWhiteSpace);
	}

	public static bool IsNotEmpty(this string value)
	{
		return value.IsNullOrEmpty() == false;
	}

	public static bool IsNull(this string value, bool considereEmptyAsNull = true)
	{
		return considereEmptyAsNull ? String.IsNullOrEmpty(value) : XHelper.Obj.IsNull(value);
	}

	public static int SafeLength(this string value)
	{
		return value?.Length ?? 0;
	}

	/// <summary>
	///     Remove any instance of the given character from the current string.
	/// </summary>
	/// <param name="value">
	///     The input.
	/// </param>
	/// <param name="removeCharc">
	///     The remove char.
	/// </param>
	/// <remarks>
	///     Contributed by Michael T, http://about.me/MichaelTran
	/// </remarks>
	public static string Remove(this string value, params char[] removeCharc)
	{
		var result = value;
		if(!String.IsNullOrEmpty(result) && removeCharc != null)
		{
			Array.ForEach(removeCharc, c => result = result.Remove(c.ToString()));
		}

		return result;
	}

	/// <summary>
	///     Remove any instance of the given string pattern from the current string.
	/// </summary>
	/// <param name="value">The input.</param>
	/// <param name="strings">The strings.</param>
	/// <returns></returns>
	/// <remarks>
	///     Contributed by Michael T, http://about.me/MichaelTran
	/// </remarks>
	public static string Remove(this string value, params string[] strings)
	{
		return strings.Aggregate(value, (current, c) => current.Replace(c, String.Empty));
	}

	public static string TrimSafe(this string value)
	{
		if(String.IsNullOrEmpty(value))
		{
			return String.Empty;
		}

		return value.Trim();
	}

	public static string Truncate(this string value, int length)
	{
		return XHelper.Strings.Truncate(value, length);
	}

    public static string ReplaceMany(this string value, params (string Parte, string NewValue)[] args) => args.Aggregate(value, (current, replace) => current.Replace(replace.Parte, replace.NewValue));
}