using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
// ReSharper disable UnusedMember.Global

// ReSharper disable CheckNamespace
namespace Grapp.Core.WebBase.Common.extensions {
    public static class XDbExtensions
// ReSharper restore CheckNamespace
    {
        public static DateTime AsDateTime(this SqlDataReader dr, string columnName)
        {
            return (DateTime)dr[columnName];
        }

        public static byte GetByte(this SqlDataReader dr, string columnName)
        {
            return (byte)dr[columnName];
        }

        public static char GetChar(this SqlDataReader dr, string columnName)
        {
            return GetString(dr, columnName).First();
        }

        public static double GetDouble(this SqlDataReader dr, string columnName)
        {
            return (double)dr[columnName];
        }

        public static float GetFloat(this SqlDataReader dr, string columnName)
        {
            return (float)dr[columnName];
        }

        public static int GetInt(this SqlDataReader dr, string columnName)
        {
            return (int)dr[columnName];
        }

        public static short GetShort(this SqlDataReader dr, string columnName)
        {
            return (short)dr[columnName];
        }

        public static string GetString(this SqlDataReader dr, string columnName)
        {
            return (string)dr[columnName];
        }

        public static SqlParameter Set(this SqlParameter param, string name, SqlDbType dbType, object value)
        {
            param.ParameterName = name;
            param.SqlDbType = dbType;
            param.Value = Translate(value);

            return param;
        }

        private static object Translate(object value)
        {
            return value ?? DBNull.Value;
        }
    }
}