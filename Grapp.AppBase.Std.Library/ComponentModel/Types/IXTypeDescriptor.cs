using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Grapp.AppBase.Std.Library.ComponentModel.Types
{
	public interface IXTypeDescriptor
	{
		Type ModelType { get; }
		string ModelDisplayName { get; }
		IEnumerable<PropertyDescriptor> Properties { get; }
		PropertyDescriptor GetDefaultLookupProperty();
		object GetDefaultLookupPropertyValue(object obj);

		string GetModelDisplayName(object obj);

		PropertyDescriptor GetPrimaryKeyProperty();
		object GetPrimaryKeyPropertyValue(object obj);
		PropertyDescriptor GetPropertyDescriptor(string propertyName);

		string GetPropertyDisplayName(PropertyDescriptor propertyDescription, bool formatRequiredAsHtml = true);

		PropertyInfo GetPropertyInfo(string propertyName);
		bool IsPropertyRequired(PropertyDescriptor propertyDescriptor);
	}

	public interface IXTypeDescriptor<TModel> : IXTypeDescriptor { }

	public interface IXVMTypeDescriptor<TModel> : IXTypeDescriptor<TModel> { }
}