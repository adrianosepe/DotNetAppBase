using System;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Abstraction
{
	public interface IDateRange
	{
		DateTime? Min { get; }

        DateTime? Max { get; }

        DateTime? MinAsBeginDay { get; }

        DateTime? MaxAsEndDay { get; }

        TimeSpan Range { get; }

        bool IsNullMin { get; }

        bool IsNullMax { get; }

        bool IsNull { get; }

        bool IsNullPartial { get; }

        bool IsHasRangeValues { get; }
	}
}