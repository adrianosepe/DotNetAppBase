using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Business
{
	public interface IEntityWorkWithConcurrency : IEntity
	{
		byte[] RowVersion { get; set; }
	}
}