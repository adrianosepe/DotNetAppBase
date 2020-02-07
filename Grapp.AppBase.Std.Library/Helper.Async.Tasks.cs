using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Grapp.AppBase.Std.Exceptions.Assert;

namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public static partial class Async
		{
			[Localizable(false)]
			public static class Tasks
			{
				public static string CreateTheadName(object context, string alias)
				{
					XContract.ArgIsNotNull(context, nameof(context));
					XContract.ArgIsNotNull(alias, nameof(alias));

					return $"#Task::{context.GetType().Name}->{alias}";
				}

				public static System.Threading.Tasks.Task Run(object context, string alias, Action actionToRun)
				{
					XContract.ArgIsNotNull(context, nameof(context));
					XContract.ArgIsNotNull(alias, nameof(alias));
					XContract.ArgIsNotNull(actionToRun, nameof(actionToRun));

					return System.Threading.Tasks.Task.Run(
						() =>
							{
								Thread.CurrentThread.Name = CreateTheadName(context, alias);

								try
								{
									actionToRun();
								}
								catch(Exception ex)
								{
									XDebug.OnException(ex);

									throw;
								}
							});
				}

				public static Task<T> Run<T>(object context, string alias, Func<T> actionToRun)
				{
					XContract.ArgIsNotNull(context, nameof(context));
					XContract.ArgIsNotNull(alias, nameof(alias));
					XContract.ArgIsNotNull(actionToRun, nameof(actionToRun));

					return System.Threading.Tasks.Task.Run(
						() =>
							{
								Thread.CurrentThread.Name = CreateTheadName(context, alias);

								try
								{
									return actionToRun();
								}
								catch(Exception ex)
								{
									XDebug.OnException(ex);

									throw;
								}
							});
				}

				public static async System.Threading.Tasks.Task Wait(TimeSpan? wait = null)
				{
					await System.Threading.Tasks.Task.Delay(wait ?? TimeSpan.FromSeconds(2));
				}
			}
		}
	}
}