using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Present
{
    public interface IPresentDisplay
    {
        string Description { get; }

        string GroupName { get; }

        string Name { get; }
    }
}