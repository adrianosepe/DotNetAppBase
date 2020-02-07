using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Types.Wrapper
{
	public class VirtualTypeDescriptor : object, ICustomTypeDescriptor
	{
		private readonly Dictionary<string, VirtualPropertyDescriptor> _data;

		public VirtualTypeDescriptor()
		{
			_data = new Dictionary<string, VirtualPropertyDescriptor>();
		}

		public IEnumerable<string> PropertiesNames => _data.Keys;

		public void Add(
			string propertyName, string displayName, Type dataType, object value,
			string category, Attribute[] attributes = null)
		{
			var newPropertyDescriptor = new VirtualPropertyDescriptor(
				propertyName, value, dataType, displayName, category, this);

			newPropertyDescriptor.AddNewAttributes(attributes);

			_data.Add(propertyName, newPropertyDescriptor);
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(attributes, true);
		}

		public PropertyDescriptorCollection GetProperties()
		{
			return new PropertyDescriptorCollection(_data.Values.Cast<PropertyDescriptor>().ToArray());
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return GetProperties();
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		public object GetValue(string propertyName)
		{
			return _data[propertyName].Value;
		}
	}
}