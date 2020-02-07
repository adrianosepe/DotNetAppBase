using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Theme.Enums;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Present
{
	public interface IPresentImageConfig
	{
		EActionImage Image { get; }

		string ImagePath { get; }
	}
}