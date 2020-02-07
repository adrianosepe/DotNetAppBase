using System;
using System.Collections.Generic;
using System.Linq;
using Grapp.ApplicationBase.Db.Contract;

namespace Grapp.ApplicationBase.Db
{
	public sealed class SqlStorage : ISqlStorage
	{
		public static readonly SqlStorage Instance;

		private readonly Dictionary<string, ISqlDatabase> _dbs;

		private ISqlDatabase _defaultDb;

		static SqlStorage()
		{
			Instance = new SqlStorage();
		}

		private SqlStorage()
		{
			_dbs = new Dictionary<string, ISqlDatabase>();
		}

		public ISqlDatabase DefaultDatabase
		{
			get => _defaultDb;
			set => _defaultDb = value;
		}

		public bool Constains(string name) => !String.IsNullOrEmpty(name) && _dbs.ContainsKey(name);

	    public ISqlDatabase Restore(string name) => Constains(name) ? _dbs[name] : null;

	    public bool Storage(ISqlDatabase database)
		{
			if(Constains(database.Name))
			{
				return false;
			}

			_dbs.Add(database.Name, database);

			return true;
		}

		public bool UnStorage(SqlDatabase dataBase)
		{
		    if(!Constains(dataBase.Name) || Restore(dataBase.Name) != dataBase)
		    {
		        return false;
		    }

		    _dbs.Remove(dataBase.Name);

		    return true;
		}
	}
}