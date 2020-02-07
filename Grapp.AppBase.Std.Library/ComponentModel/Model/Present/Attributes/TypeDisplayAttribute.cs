using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Present.Attributes.Base;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Present.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class TypeDisplayAttribute : BaseDisplayAttribute, IPresentDisplay, IDisplayType
    {
        string IPresentDisplay.Description => GetDescription();

        string IPresentDisplay.Name => GetName();

        string IPresentDisplay.GroupName => GetGroupName();
    }
}