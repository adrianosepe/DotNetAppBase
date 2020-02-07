using System;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Assert;

namespace Grapp.AppBase.Std.Library.ComponentModel.Collection.Events
{
	public class EntityActionEventArgs<TEntity> : EventArgs where TEntity : class
	{
		public EntityActionEventArgs(TEntity entity)
		{
			XContract.ArgIsNotNull(entity, nameof(entity));

			Entity = entity;
			Action = EEntityAction.NotInformed;
		}

		public EntityActionEventArgs(TEntity entity, EEntityAction action) : this(entity)
		{
			Action = action;
		}

		public TEntity Entity { get; }

		public EEntityAction Action { get; }
	}
}