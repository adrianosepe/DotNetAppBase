using System;
using System.Threading.Tasks;
// ReSharper disable UnusedMemberInSuper.Global

namespace Grapp.AppBase.Std.Library.Tasks
{
	public interface ITask : IDisposable
	{
		bool AutoCatchException { get; set; }
	    
		bool Enabled { get; }

		bool Start();
	    
		Task<bool> Stop();
	}
}