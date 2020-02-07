using System.Collections.Concurrent;

// ReSharper disable CheckNamespace
public static class XConcurrentQueueExtensions
// ReSharper restore CheckNamespace
{
	public static void Clear<T>(this ConcurrentQueue<T> queue)
	{
		T item;
		while(queue.TryDequeue(out item))
		{
			// do nothing
		}
	}
}