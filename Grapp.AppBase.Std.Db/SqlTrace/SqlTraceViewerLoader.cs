using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Grapp.ApplicationBase.Db.SqlTrace
{
	public static class SqlTraceViewerLoader
	{
		public const string AssemblyPattern = "Grapp.ApplicationBase.Db.TraceViewer*.dll";

		private static ISqlTraceViewerRegister _register;

		public static void Load()
		{
			if(_register != null)
			{
				return;
			}

			var files = Directory.GetFiles(path: Directory.GetCurrentDirectory(), AssemblyPattern, SearchOption.TopDirectoryOnly);

			foreach(var assembly in files.Select(Assembly.LoadFile))
			{
				_register = assembly.CreateInstance(typeName: "SqlTraceViewerRegister") as ISqlTraceViewerRegister;
				if(_register != null)
				{
					SqlTraceProvider.Instance.SqlTrace += _register.SqlTraceHandler;
				}
			}
		}

		public static void Unload()
		{
		    if(_register == null)
		    {
		        return;
		    }

		    _register.Dispose();

		    SqlTraceProvider.Instance.SqlTrace -= _register.SqlTraceHandler;
		}
	}
}