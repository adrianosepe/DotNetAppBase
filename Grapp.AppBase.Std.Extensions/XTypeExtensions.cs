using System;
using System.Collections.Generic;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Assert;
using Grapp.AppBase.Std.Library;
using Grapp.AppBase.Std.Library.Attributes;

// ReSharper disable CheckNamespace
public static class XTypeExtensions
// ReSharper restore CheckNamespace
{
	public static TAttribute GetAttribute<TAttribute>(this Type type) where TAttribute : Attribute
	{
		XContract.ArgIsNotNull(type, argumentName: nameof(type));

		return XHelper.Reflections.Attributes.Get<TAttribute>(type);
	}

	public static bool Is(this Type implementation, params Type[] contractsTypes)
	{
		return contractsTypes.All(contract => XHelper.Types.Is(contract, implementation));
	}

	public static IEnumerable<Type> IsClass(this IEnumerable<Type> types)
	{
		return types.Where(type => type.IsClass);
	}

	public static IEnumerable<Type> IsVisible(this IEnumerable<Type> types)
	{
		return types.Where(type => !XHelper.Reflections.Attributes.HasAttribute<HideTypeAttribute>(type, inherit: false));
	}

	public static IEnumerable<Type> HasAttribute<TAttribute>(this IEnumerable<Type> types) where TAttribute : Attribute
	{
		return types.Where(type => XHelper.Reflections.Attributes.HasAttribute<TAttribute>(type, inherit: false));
	}
}