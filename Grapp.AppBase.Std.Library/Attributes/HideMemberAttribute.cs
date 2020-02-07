using System;

namespace Grapp.AppBase.Std.Library.Attributes 
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false)]
    public sealed class HideMemberAttribute : System.Attribute { }
}