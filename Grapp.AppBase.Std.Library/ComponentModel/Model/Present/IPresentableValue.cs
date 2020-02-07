using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Present
{
	public interface IPresentableValue : IPresentable
	{
		decimal Value { get; }
	}
}