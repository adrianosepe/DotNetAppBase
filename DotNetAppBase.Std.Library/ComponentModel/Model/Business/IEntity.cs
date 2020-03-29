using System.ComponentModel.DataAnnotations;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Business
{
    public interface IEntity
    {
        [Display(Name = "Código")]
        int ID { get; set; }

        [Display(Name = "Apresentação")]
        string Display { get; }
    }
}