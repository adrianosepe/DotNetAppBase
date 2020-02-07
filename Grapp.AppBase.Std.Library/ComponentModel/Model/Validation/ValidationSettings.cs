using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Enums;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation
{
    public static class ValidationSettings
    {
        public static bool InParaguayMode => RestrictionFor == EModoOperacao.Paraguai;

        public static EModoOperacao RestrictionFor { get; set; }
    }
}