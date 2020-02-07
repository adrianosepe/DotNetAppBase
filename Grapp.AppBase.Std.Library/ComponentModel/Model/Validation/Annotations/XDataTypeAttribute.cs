using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations
{
	public abstract class XDataTypeAttribute : DataTypeAttribute
	{
		protected XDataTypeAttribute(EDataType dataType) 
            : base(Translate(dataType))
        {
            DataType = dataType;
        }

        public new EDataType DataType { get; }

        public override string GetDataTypeName()
        {
            if (base.DataType == System.ComponentModel.DataAnnotations.DataType.Custom)
            {
                return "Custom";
            }

            return base.GetDataTypeName();
        }

        private static DataType Translate(EDataType dataType)
        {
            var translated = XHelper.Enums.TryGetValue<DataType>(dataType.ToString());

            return translated ?? System.ComponentModel.DataAnnotations.DataType.Custom;
        }

        public override bool IsValid(object value) => InternalIsValid(value);

        protected abstract bool InternalIsValid(object value);
    }
}