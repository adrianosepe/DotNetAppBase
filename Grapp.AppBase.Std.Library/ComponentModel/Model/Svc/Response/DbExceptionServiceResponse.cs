using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Svc
{
	[DataContract, Serializable]
	public class DbExceptionServiceResponse<T> : ServiceResponse<T> where T : class
	{
		public DbExceptionServiceResponse(DbException exception) : this(null, exception) { }

		public DbExceptionServiceResponse(T entity, DbException exception) : base(entity, CreateResult(exception)) { }

		private static EntityValidationResult CreateResult(DbException exception) => new EntityValidationResult(new[] {new ValidationResult(exception.Message)});
    }
}