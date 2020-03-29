using System;
using System.ComponentModel;
using System.Configuration;
using DotNetAppBase.Std.Library.Settings;

namespace DotNetAppBase.Std.Library
{
	public static partial class XHelper
	{
		public sealed class ConfigWrapper<TKey, TValue> where TValue : class
		{
			private readonly Func<TKey, TValue> _translate;

			internal ConfigWrapper(Func<TKey, TValue> translate) => _translate = translate;

            public TValue this[TKey name] => _translate(name);

			public TValue Get(TKey key, TValue defaultValue) => this[key] ?? defaultValue;
		}

		public static class Configs
		{
#if !NETSTANDARD
            public static readonly ConfigWrapper<string, string> AppSettings = new ConfigWrapper<string, string>(s => ConfigurationManager.AppSettings[s]);

			public static readonly ConfigWrapper<string, string> ConnStrings = new ConfigWrapper<string, string>(s => ConfigurationManager.ConnectionStrings[s].ConnectionString);

			public static readonly ConfigWrapper<string, ConnectionStringSettings> ConnStringsSettings = new ConfigWrapper<string, ConnectionStringSettings>(s => ConfigurationManager.ConnectionStrings[s]);
#endif

            [Localizable(false)]
            public static SettingsBuilder XAppSettings(string sectionID) => new SettingsBuilder(sectionID, "AppSettings");
		}
	}
}