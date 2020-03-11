using System;
using System.Data.Common;
using System.Linq;

namespace Grapp.AppBase.Std.Exceptions.Base
{
	public class XDbException : XException
	{
		protected const string Prefix = "XDbException:";

		protected XDbException(DbException ex, string message) : base(message: message.Replace(Prefix, String.Empty), ex) { }

		public XDbException(DbException ex) : base(message: ex.Message.Replace(Prefix, String.Empty), ex)
		{
			if(!ex.Message.StartsWith(Prefix))
			{
				throw new XException(
					message: "A exceção XDbException requer como base uma exceção disparada pelo " +
					"banco de dados, e esta exceção será assumida como parâmetro.");
			}
		}

		protected XDbException(string exception, Exception innerException = null) : base(message: $"{Prefix}{exception}", innerException) { }

		public static string FormatMessage(Exception exception)
		{
			if(exception is DbException && exception.Message.StartsWith(Prefix))
			{
				return exception.Message.Replace(Prefix, String.Empty);
			}

			return exception.Message;
		}

		public static bool IsOne(Exception exception, out XDbException xException)
		{
			xException = null;

			if(exception is DbException sqlEx)
			{
			    if(exception.Message.StartsWith(Prefix))
				{
					xException = new XDbException(sqlEx);
				}
				else
				{
					var result = exception.Message.Split(separator: new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
					if(result.Length > 1 && result[1].StartsWith(value: "XDbException:"))
					{
						xException = new XDbException(sqlEx, message: result[1].Replace(oldValue: "XDbException:", String.Empty));
					}
				}
			}

			return xException != null;
		}
	}
}