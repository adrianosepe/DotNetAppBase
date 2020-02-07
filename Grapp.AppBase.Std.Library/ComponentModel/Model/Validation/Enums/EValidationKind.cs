using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Grapp.AppBase.Std.Library.Properties;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Enums
{
	public enum EValidationKind
	{
        [Display(ResourceType = typeof(DbEnums), Name = "EValidationKind_Error")]
		Error,

	    [Display(ResourceType = typeof(DbEnums), Name = "EValidationKind_Warning")]
		Warning
	}
}