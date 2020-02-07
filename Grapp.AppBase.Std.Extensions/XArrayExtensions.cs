using System;
using System.Linq;
using Grapp.AppBase.Std.Library;

// ReSharper disable CheckNamespace
public static class XArrayExtensions
// ReSharper restore CheckNamespace
{
	public static void ForEach<T>(this T[] array, Action<T, int> action)
	{
		if(array == null)
		{
			throw new ArgumentNullException(paramName: nameof(array));
		}

		if(action == null)
		{
			throw new ArgumentNullException(paramName: nameof(action));
		}

		for(var i = 0; i < array.Length; i++)
		{
			action(arg1: array[i], i);
		}
	}

	public static bool IsEmpty<T>(this T[] array)
	{
		return XHelper.Arrays.IsEmpty(array);
	}

	public static bool IsNullOrEmpty<T>(this T[] array)
	{
		return XHelper.Arrays.IsNullOrEmpty(array);
	}

	public static bool IsThere<T>(this T[] array, int index)
	{
		return array != null && index >= 0 && index <= array.Length - 1;
	}

	public static bool TryGet<T>(this T[] array, int index, out T item)
	{
		if(!array.IsThere(index))
		{
			item = default(T);
			return false;
		}

		item = array[index];
		return true;
	}
}