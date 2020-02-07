using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Assert.Debugs;

namespace Grapp.AppBase.Std.Exceptions.Assert
{
	public static class XDebug
	{
		private static readonly List<IKnowledgeableExceptions> KnowledgeableExceptionsList;

		static XDebug()
		{
			KnowledgeableExceptionsList = new List<IKnowledgeableExceptions>();
		}

		[Conditional(conditionString: "DEBUG")]
		public static void OnException(Exception exception)
		{
			if(exception == null)
			{
				return;
			}

			var message = GetExceptionMessage(exception);

			Debug.Assert(condition: true, message);
		}

		public static void RegisterKnowledgeableExceptions(IKnowledgeableExceptions knowledgeableExceptions)
		{
			if(KnowledgeableExceptionsList.Contains(knowledgeableExceptions))
			{
				return;
			}

			KnowledgeableExceptionsList.Add(knowledgeableExceptions);
		}

		private static string GetExceptionMessage(Exception exception)
		{
			foreach(var knowledgeableExceptionse in KnowledgeableExceptionsList)
			{
				string message;
				if(knowledgeableExceptionse.DoYouKnowAboutMessage(exception, out message))
				{
					return message;
				}
			}

			return exception.Message;
		}
	}
}