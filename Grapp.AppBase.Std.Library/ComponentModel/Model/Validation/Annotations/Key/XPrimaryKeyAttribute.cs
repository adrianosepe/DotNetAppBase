﻿using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Key
{
	public class XPrimaryKeyAttribute : XDataTypeAttribute
	{
		public XPrimaryKeyAttribute() : base(EDataType.PrimaryKey) { }

        protected override bool InternalIsValid(object value) => true;
    }
}