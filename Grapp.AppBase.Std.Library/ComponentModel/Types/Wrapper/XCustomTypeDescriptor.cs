using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Base;
using NativeTypeDesc = System.ComponentModel.TypeDescriptor;

namespace Grapp.AppBase.Std.Library.ComponentModel.Types.Wrapper
{
	public class XCustomTypeDescriptor : ICustomTypeDescriptor
	{
		private readonly object _entity;

		private readonly Type _entityType;

		private readonly Lazy<PropertyDescriptorCollection> _lazyProperties;

		public XCustomTypeDescriptor(object entity)
		{
			_entity = entity;
			_entityType = _entity.GetType();
			TypeDescriptor = XTypeDescriptor.Get(_entityType);

			_lazyProperties = new Lazy<PropertyDescriptorCollection>(
				() =>
					{
						var properties = new List<XPropertyDescriptor>();

						properties.AddRange(
							TypeDescriptor.PropertiesOfMetadataPropertyDescriptor
								.Select(prop => new XPropertyDescriptor(prop, null, this)));

						return new PropertyDescriptorCollection(properties.Cast<PropertyDescriptor>().ToArray());
					});
		}

		public XTypeDescriptor TypeDescriptor { get; }

		public void AddAttributesToProperty(string propertyName, Attribute[] customAttributes)
		{
			var propertyDescriptor = GetProperties()
				.Find(propertyName, true)
				.FlowMustBeNotNull()
				.As<XPropertyDescriptor>();

			if(propertyDescriptor == null)
			{
				throw new XException($"The property description must be of type {nameof(XPropertyDescriptor)}");
			}

			propertyDescriptor.AddNewAttributes(customAttributes);
		}

		public AttributeCollection GetAttributes()
		{
			return NativeTypeDesc.GetAttributes(_entityType, true);
		}

		public string GetClassName()
		{
			return NativeTypeDesc.GetClassName(_entityType, true);
		}

		public string GetComponentName()
		{
			return NativeTypeDesc.GetComponentName(_entityType, true);
		}

		public TypeConverter GetConverter()
		{
			return NativeTypeDesc.GetConverter(_entityType, true);
		}

		public EventDescriptor GetDefaultEvent()
		{
			return NativeTypeDesc.GetDefaultEvent(_entityType, true);
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return NativeTypeDesc.GetDefaultProperty(_entityType, true);
		}

		public object GetEditor(Type editorBaseType)
		{
			return NativeTypeDesc.GetEditor(_entityType, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return NativeTypeDesc.GetEvents(_entityType, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return NativeTypeDesc.GetEvents(attributes, true);
		}

		public PropertyDescriptorCollection GetProperties()
		{
			return _lazyProperties.Value;
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return GetProperties();
		}

		public object GetPropertyOwner(PropertyDescriptor propertyDescriptor)
		{
			return _entity;
		}
	}
}