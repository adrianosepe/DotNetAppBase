using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Present
{
    public interface IDisplayType
    {
        string DisplayPattern { get; }

        string Name { get; } 

        string Description { get; }

        int Level { get; }
    }
}