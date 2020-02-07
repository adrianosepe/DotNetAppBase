using System.ComponentModel.DataAnnotations;
using Grapp.AppBase.Std.Library.Properties;

namespace Grapp.AppBase.Std.Library.Files
{
	public enum EFileExtension
	{
		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Unknown")]
		Unknown,

		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Xls")]
		Xls,

		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Xlsx")]
		Xlsx,

		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Pdf")]
		Pdf,

		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Txt")]
		Txt,

		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Doc")]
		Doc,

		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Docx")]
		Docx,

		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Png")]
		Png,

		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Xml")]
		Xml,

	    [Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Eml")]
	    Eml,

		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Any")]
		Any,

		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_MultiExtensions")]
		MultiExtensions,

		[Display(ResourceType = typeof(DbEnums), Name = "EFileExtension_Csv")]
        Csv
    }
}