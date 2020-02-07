using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Business
{
    public interface IEntity
    {
        [Display(Name = "Código")]
        int ID { get; set; }

        [Display(Name = "Apresentação")]
        string Display { get; }
    }
}