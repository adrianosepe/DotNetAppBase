// ReSharper disable UnusedMember.Global
namespace Grapp.AppBase.Std.Library.Tasks.Worker
{
	public interface IWorkerDequeue<T> : IWorker<T>
	{
		void Keepalive();
	}
}