using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString
{
    public class XNameAttribute : XMaxLengthAttribute
    {
        public XNameAttribute() : base(80) { }
    }
}