using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Present
{
	public interface IPresentableWithCode : IPresentable
	{
		string Code { get; }
	}
}