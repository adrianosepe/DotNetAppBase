using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable BuiltInTypeReferenceStyle
namespace Grapp.AppBase.Std.Library
{
    public partial class XHelper
    {
        [Localizable(false)]
        public static class Sql
        {
            private static readonly Dictionary<Type, SqlDbType> MapClrToDbType = new Dictionary<Type, SqlDbType>
                {
                    {typeof(Boolean), SqlDbType.Bit},
                    {typeof(String), SqlDbType.NVarChar},
                    {typeof(DateTime), SqlDbType.DateTime},
                    {typeof(Int16), SqlDbType.SmallInt},
                    {typeof(Int32), SqlDbType.Int},
                    {typeof(Int64), SqlDbType.BigInt},
                    {typeof(Decimal), SqlDbType.Float},
                    {typeof(Double), SqlDbType.Decimal},
                    {typeof(Byte[]), SqlDbType.Binary}
                };

            private static readonly Dictionary<SqlDbType, Type> MapDbTypeToClr = new Dictionary<SqlDbType, Type>
                {
                    {SqlDbType.BigInt, typeof(Int64)},
                    {SqlDbType.Binary, typeof(Byte[])},
                    {SqlDbType.Bit, typeof(Boolean)},
                    {SqlDbType.Char, typeof(String)},
                    {SqlDbType.Date, typeof(DateTime)},
                    {SqlDbType.DateTime, typeof(DateTime)},
                    {SqlDbType.DateTime2, typeof(DateTime)},
                    {SqlDbType.DateTimeOffset, typeof(DateTimeOffset)},
                    {SqlDbType.Decimal, typeof(Decimal)},
                    {SqlDbType.Float, typeof(Double)},
                    {SqlDbType.Image, typeof(Byte[])},
                    {SqlDbType.Int, typeof(Int32)},
                    {SqlDbType.Money, typeof(Decimal)},
                    {SqlDbType.NChar, typeof(String)},
                    {SqlDbType.NText, typeof(String)},
                    {SqlDbType.NVarChar, typeof(String)},
                    {SqlDbType.Real, typeof(Single)},
                    {SqlDbType.SmallDateTime, typeof(DateTime)},
                    {SqlDbType.SmallInt, typeof(Int16)},
                    {SqlDbType.SmallMoney, typeof(Decimal)},
                    {SqlDbType.Text, typeof(String)},
                    {SqlDbType.Time, typeof(TimeSpan)},
                    {SqlDbType.Timestamp, typeof(Byte[])},
                    {SqlDbType.TinyInt, typeof(Byte)},
                    {SqlDbType.UniqueIdentifier, typeof(Guid)},
                    {SqlDbType.VarBinary, typeof(Byte[])},
                    {SqlDbType.VarChar, typeof(String)}
                };

            public static string GetAssemblyHexString(string assemblyPath)
            {
                if(!Path.IsPathRooted(assemblyPath))
                {
                    assemblyPath = Path.Combine(Environment.CurrentDirectory, assemblyPath);
                }

                var builder = new StringBuilder();
                builder.Append("0x");

                using(var stream = new FileStream(assemblyPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var currentByte = stream.ReadByte();
                    while(currentByte > -1)
                    {
                        builder.Append(currentByte.ToString("X2", CultureInfo.InvariantCulture));
                        currentByte = stream.ReadByte();
                    }
                }

                return builder.ToString();
            }

            public static Type GetClrType(SqlDbType dbType, bool asNullable = false)
            {
                MapDbTypeToClr.TryGetValue(dbType, out var clrType);

                if(asNullable && clrType != null && clrType.IsValueType)
                {
                    clrType = Types.MakeGenericType(typeof(Nullable<>), clrType);
                }

                return clrType;
            }

            public static SqlDbType GetDbType(Type clrType)
            {
                if(Types.IfNullable(clrType, out var underlyingType))
                {
                    clrType = underlyingType;
                }

                MapClrToDbType.TryGetValue(clrType, out var dbType);

                return dbType;
            }

            public static TResult GetFromDbValue<TResult>(object dbValue) => Converts.ConvertTo<TResult>(dbValue);

            public static IEnumerable<string> SplitScriptByLines(string script, int countLines)
            {
                var count = 0;
                var builder = new StringBuilder();
                using(var reader = new StringReader(script))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        builder.AppendLine(line);
                        if(++count == countLines)
                        {
                            yield return builder.ToString();

                            count = 0;
                            builder.Clear();
                        }
                    }
                }

                if(builder.Length > 0)
                {
                    yield return builder.ToString();
                }
            }

            public static IEnumerable<string> SplitScriptByMarker(string script, string marker)
            {
                var pattern = marker;

                var matcher = new Regex(pattern, RegexOptions.Compiled);
                var start = 0;
                var batch = matcher.Match(script);

                while(batch.Success)
                {
                    var end = batch.Index;

                    var partialScript = script.Substring(start, end - start).Trim();

                    if(Strings.HasData(partialScript))
                    {
                        yield return partialScript;
                    }

                    start = end + batch.Length;
                    batch = matcher.Match(script, start);
                }

                if(script.Length > start)
                {
                    var partialScript = script.Substring(start, script.Length - start).Trim();
                    if(Strings.HasData(partialScript))
                    {
                        yield return partialScript;
                    }
                }
            }

            public static object ToDbValue(object @object)
            {
                return @object ?? DBNull.Value;
            }
        }
    }
}