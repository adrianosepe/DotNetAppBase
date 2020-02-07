using System;
using System.Linq;
using Grapp.AppBase.Std.Library;

// ReSharper disable CheckNamespace
namespace Grapp.AppBase.Std.ComponentModel.Model.Extensions {
    public static class XForeignKeyExtensions
// ReSharper restore CheckNamespace
    {
        public static bool FkIsNotNull(this int fk) => XHelper.Models.FkIsNotNull(fk);

        public static bool FkIsNull(this int fk) => XHelper.Models.FkIsNull(fk);
    }
}