using System;
using System.Linq;

namespace Grapp.AppBase.Std.Exceptions.Assert.Debugs
{
	public interface IKnowledgeableExceptions
	{
		bool DoYouKnowAboutMessage(Exception ex, out string message);
	}
}