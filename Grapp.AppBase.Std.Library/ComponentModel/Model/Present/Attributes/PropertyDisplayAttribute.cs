using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Present.Attributes.Base;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Present.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyDisplayAttribute : BaseDisplayAttribute, IPresentDisplay
    {
        string IPresentDisplay.Description => GetDescription();

        string IPresentDisplay.Name => GetName();

        string IPresentDisplay.GroupName => GetGroupName();
    }
}