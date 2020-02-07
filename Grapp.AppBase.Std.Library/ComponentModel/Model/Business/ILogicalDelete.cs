using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Business.Enums;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Business
{
    public interface ILogicalDelete
    {
        ELogicalDelete Situacao { get; set; }
    }
}