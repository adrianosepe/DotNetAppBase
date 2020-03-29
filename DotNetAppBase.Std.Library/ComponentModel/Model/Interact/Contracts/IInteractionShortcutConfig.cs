using DotNetAppBase.Std.Library.ComponentModel.Model.Interact.Enums;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Interact.Contracts
{
	public interface IInteractionShortcutConfig
	{
        EKey Key { get; }

		bool Control { get; }

		bool Alt { get; }

		bool Shift { get; }
	}
}