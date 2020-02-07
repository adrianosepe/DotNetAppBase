using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Types.Wrapper
{
	public class XPropertyDescriptor : PropertyDescriptor
	{
		private readonly XCustomTypeDescriptor _customTypeDescriptor;

		public XPropertyDescriptor(PropertyDescriptor descriptor, Attribute[] attributes, XCustomTypeDescriptor customTypeDescriptor) : base(descriptor, attributes)
		{
			Descriptor = descriptor;
			_customTypeDescriptor = customTypeDescriptor;
		}

		public PropertyDescriptor Descriptor { get; }

		public override Type PropertyType => Descriptor.PropertyType;
		public override Type ComponentType => Descriptor.ComponentType;

		public override string DisplayName => _customTypeDescriptor.TypeDescriptor.GetPropertyDisplayName(Descriptor, false);

		public override bool IsReadOnly
		{
			get
			{
				var readOnlyAttribute = (ReadOnlyAttribute)Attributes[typeof(ReadOnlyAttribute)];

				return readOnlyAttribute?.IsReadOnly ?? Descriptor.IsReadOnly;
			}
		}

		public bool IsRequired => _customTypeDescriptor.TypeDescriptor.IsPropertyRequired(Descriptor);

		public override bool CanResetValue(object component)
		{
			return Descriptor.CanResetValue(component);
		}

		public override object GetValue(object component)
		{
			return Descriptor.GetValue(component);
		}

		public override void ResetValue(object component)
		{
			Descriptor.ResetValue(component);
		}

		public override void SetValue(object component, object value)
		{
			Descriptor.SetValue(component, value);
		}

		public override bool ShouldSerializeValue(object component)
		{
			return Descriptor.ShouldSerializeValue(component);
		}

		public void AddNewAttributes(Attribute[] attributes)
		{
			var newAttrs = new List<Attribute>();

			newAttrs.AddRange(AttributeArray);
			newAttrs.AddRange(attributes);

			AttributeArray = newAttrs.ToArray();
		}
	}
}