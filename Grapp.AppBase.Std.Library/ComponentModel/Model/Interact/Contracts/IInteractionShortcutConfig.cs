using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Interact.Enums;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Interact.Contracts
{
	public interface IInteractionShortcutConfig
	{
        EKey Key { get; }

		bool Control { get; }

		bool Alt { get; }

		bool Shift { get; }
	}
}