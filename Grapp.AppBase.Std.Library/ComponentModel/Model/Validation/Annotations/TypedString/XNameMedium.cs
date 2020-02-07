using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString
{
    public class XNameMedium : XMaxLengthAttribute
    {
        public XNameMedium() : base(120) { }
    }
}