using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Enums;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Behaviors
{
	public interface IDateTimeConstraint
	{
		EDateTimeFormat Format { get; }
	}
}