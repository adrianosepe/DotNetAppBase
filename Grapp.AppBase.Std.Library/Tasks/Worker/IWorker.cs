﻿using System;

namespace Grapp.AppBase.Std.Library.Tasks.Worker
{
	public interface IWorker<T> : ITask
	{
		string Name { get; }

		void Configure(string workerName, Func<T> readData, Action<T> processData);
	}
}