using System;
using System.Linq;

namespace Grapp.AppBase.Std.Exceptions.Bussines
{
	public class XDataNotFoundException : XBussinesException
	{
		private const string DefaultMessage = "The entity {0} is not found using criteria {1}";

		protected XDataNotFoundException(string entityName, string criteria) : base(message: String.Format(DefaultMessage, entityName, criteria)) { }

		public static XDataNotFoundException Create(string entityName, string criteria) => new XDataNotFoundException(entityName, criteria);
	}
}