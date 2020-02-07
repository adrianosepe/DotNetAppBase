using System;
using System.Linq;
using Grapp.AppBase.Std.Library;

// ReSharper disable CheckNamespace
internal static class InternalStringExtensions
// ReSharper restore CheckNamespace
{
    public static bool IsEmptyOrWhiteSpace(this string value) => XHelper.Strings.IsEmptyOrWhiteSpace(value);
}