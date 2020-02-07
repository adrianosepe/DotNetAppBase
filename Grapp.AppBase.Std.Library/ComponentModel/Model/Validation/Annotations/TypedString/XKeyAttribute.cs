using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString
{
    public class XKeyAttribute : XMaxLengthAttribute
    {
        public XKeyAttribute() : base(20) { }
    }
}