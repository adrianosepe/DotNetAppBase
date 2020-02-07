using System;
using System.Data.Common;
using System.Linq;

namespace Grapp.ApplicationBase.Db.SqlTrace
{
    public class SqlTraceProvider : object, IDisposable
    {
        public static readonly SqlTraceProvider Instance;

        private readonly object _objSync = new object();

        private bool _disposed;
        private bool _loaded;

        static SqlTraceProvider()
        {
            Instance = new SqlTraceProvider();
        }

        public bool AllowTrace { get; set; }

        public event SqlTraceEventHandler SqlTrace;

        public void Trace(SqlAccess access, DbCommand command)
        {
            if(!AllowTrace)
            {
                return;
            }

            if(!_loaded)
            {
                Load();
            }

            OnSqlTrace(access.Calller, command, access.Session.Database.ConnectionString);
        }

        private void Load()
        {
            lock(_objSync)
            {
                if(_loaded)
                {
                    return;
                }

                SqlTraceViewerLoader.Load();

                _loaded = true;
            }
        }

        private void OnSqlTrace(object sender, DbCommand command, string connectionString)
        {
            SqlTrace?.Invoke(sender, e: new SqlTraceEventArgs(command, connectionString));
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        private void Dispose(bool disposing)
        {
            if(_disposed)
            {
                return;
            }

            if(disposing)
            {
                GC.SuppressFinalize(obj: this);
            }

            if(AllowTrace && _loaded)
            {
                SqlTraceViewerLoader.Unload();

                _loaded = false;
                AllowTrace = false;
            }

            _disposed = true;
        }

        ~SqlTraceProvider()
        {
            Dispose(disposing: false);
        }

        #endregion
    }
}