using System;
using System.Linq.Expressions;
using Grapp.AppBase.Std.Library;

// ReSharper disable CheckNamespace
public static class XExpressionExtensions
// ReSharper restore CheckNamespace
{
	public static string GetMemberName<T>(this T obj, Expression<Func<T, object>> expression)
	{
		return XHelper.Expressions.GetMemberName(expression);
	}

	public static string GetMemberName<T>(this Expression<Func<T, object>> expression)
	{
		return XHelper.Expressions.GetMemberName(expression);
	}

	public static string GetMemberName<T>(this LambdaExpression expression)
	{
		return XHelper.Expressions.GetMemberName(expression);
	}
}