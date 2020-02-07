using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;
using Grapp.AppBase.Std.Library.Properties;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString
{
	public class XMaxLengthAttribute : XValidationAttribute, IMaxLengthConstraint
	{
		public XMaxLengthAttribute(int value) : this(EDataType.Custom, value) { }

		public XMaxLengthAttribute(EDataType dataType, int value) 
		    : base(dataType, EValidationMode.Custom, String.Format(DbMessages.XMaxLengthAttribute_Message, "{0}", value))
		{
			Value = value;
		}

		public int Value { get; }

		public override string Mask => null;

	    protected override bool InternalIsValid(object value)
		{
			if(value == null)
			{
				return true;
			}

			var input = value.As<string>();

			if(input.IsEmptyOrWhiteSpace())
			{
				return true;
			}

			return input.Length <= Value;
		}
	}
}