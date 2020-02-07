using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Grapp.AppBase.Std.Exceptions.Assert;
using Grapp.AppBase.Std.Library;

// ReSharper disable CheckNamespace
[Localizable(isLocalizable: false)]
public static partial class XObjectExtensions
// ReSharper restore CheckNamespace
{
	// #region Casts

	public static object Box<T>(this T obj) where T : struct => obj;

    public static T As<T>(this object obj) where T : class => obj as T;

    public static T CastOrDefault<T>(this object value, T defaultValue)
	{
		if(Equals(value, objB: null))
		{
			return defaultValue;
		}

		return (T)value;
	}

	public static T CastTo<T>(this object value) => (T)value;

    // #region Converts

	public static T ConvertTo<T>(this object value, T defaultValue = default(T))
	{
		if(value == null)
		{
			return defaultValue;
		}

		var targetType = typeof(T);

		if(value.GetType() == targetType)
		{
			return (T)value;
		}

		var converter = TypeDescriptor.GetConverter(value);
		if(converter.CanConvertTo(targetType))
		{
			return (T)converter.ConvertTo(value, targetType);
		}

		converter = TypeDescriptor.GetConverter(targetType);
		if(converter.CanConvertFrom(sourceType: value.GetType()))
		{
			return (T)converter.ConvertFrom(value);
		}

		return defaultValue;
	}

	// #region Expression

	public static Expression<Func<T, TResult>> ExpressionFor<T, TResult>(this T obj, Expression<Func<T, TResult>> expression) => expression;

    public static TAttribute GetAttribute<TAttribute>(this object obj) where TAttribute : Attribute
	{
		if(obj == null)
		{
			throw new ArgumentNullException(paramName: nameof(obj));
		}

		return XHelper.Reflections.Attributes.Get<TAttribute>(objType: obj.GetType());
	}

	public static bool Is<T>(this object obj) where T : class => obj is T;

    public static bool Is<T>(this object obj, out T @as) where T : class
	{
		@as = obj as T;
		return @as != null;
	}

	public static bool IsDbNull(this object obj) => obj == DBNull.Value;

    public static bool IsEqual<T>(this T objA, T objB) => XHelper.Obj.AreEquals(objA, objB);

    public static bool IsNotEqual<T>(this T value, T valueToCompare) => !IsEqual(value, valueToCompare);

    public static bool IsNotNull(this object target) => !IsNull(target);

    // #region Check Is Null Or Not Is Null

	public static bool IsNull(this object obj) => XHelper.Obj.IsNull(obj);

    // #region Method

	public static object MethodInvoke(this object obj, string methodName, params object[] arguments) => XHelper.Reflections.Methods.Invoke(obj, methodName, arguments);

    public static object MethodInvokeGeneric(this object obj, string methodName, Type[] genericTypes, params object[] arguments) 
        => XHelper.Reflections.Methods.InvokeGeneric(obj, methodName, genericTypes, arguments);

    // #region Property

	public static PropertyInfo PropertyInfo<T>(this T obj, Expression<Func<T, object>> expression)
	{
		XContract.ArgIsNotNull(obj, argumentName: nameof(obj));
		XContract.ArgIsNotNull(expression, argumentName: nameof(expression));

		return XHelper.Reflections.Properties.Get(obj, propertyName: XHelper.Expressions.GetMemberName(expression));
	}

	public static object PropertyReadValue(this object obj, string propertyName) => XHelper.Reflections.Properties.ReadValue<object>(obj, propertyName);

    public static object PropertyReadValue(this object obj, PropertyInfo propertyInfo) => XHelper.Reflections.Properties.ReadValue<object>(obj, propertyInfo);

    public static object PropertyReadValueIfExists(this object obj, [Localizable(isLocalizable: false)] string propertyName) => XHelper.Reflections.Properties.ReadValueIfExists<object>(obj, propertyName);

    public static void PropertyWriteValue(this object obj, string propertyName, object newValue) => XHelper.Reflections.Properties.WriteValue(obj, propertyName, newValue);

    public static void PropertyWriteValueIfExists(this object obj, string propertyName, object newValue) => XHelper.Reflections.Properties.WriteValueIfExists(obj, propertyName, newValue);
}