using System;
using System.Reflection;
using Grapp.AppBase.Std.Exceptions.Assert;
using Grapp.AppBase.Std.Exceptions.Contract;

namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public partial class Reflections
		{
			public static class Fields
			{
				public const BindingFlags InstanceBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
				public const BindingFlags StaticBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

				public static FieldInfo Get(object obj, string fieldName, bool ifNotFoundThrowAnException = true)
				{
					return Get(obj.GetType(), fieldName, InstanceBindingFlags, ifNotFoundThrowAnException);
				}

				public static FieldInfo GetStatic(Type type, string fieldName, bool ifNotFoundThrowAnException = true)
				{
					return Get(type, fieldName, StaticBindingFlags, ifNotFoundThrowAnException);
				}

				public static T ReadValue<T>(object obj, string fieldName)
				{
					return (T)Get(obj, fieldName).GetValue(obj);
				}

				public static void WriteValue(object obj, string fieldName, object newValue)
				{
					var field = Get(obj, fieldName, true);
					field.SetValue(obj, newValue);
				}

				public static void WriteValueIfPropertyExists(object obj, string fieldName, object newValue)
				{
					var field = Get(obj, fieldName, false);
					field?.SetValue(obj, newValue);
				}

				private static FieldInfo Get(Type type, string fieldName, BindingFlags bindingFlags, bool ifNotFoundThrowAnException = true)
				{
					XContract.ArgIsNotNull(fieldName, nameof(fieldName));
					XContract.ArgIsNotNull(type, nameof(type));

					var fieldInfo = type.GetField(fieldName, bindingFlags);

					if(fieldInfo == null && ifNotFoundThrowAnException)
					{
						throw XArgumentException.Create(nameof(fieldName), $"Field '{fieldName}' not found on type {type.Name}.");
					}

					return fieldInfo;
				}
			}
		}
	}
}