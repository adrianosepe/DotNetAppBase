using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using Grapp.AppBase.Std.Library.Data.Udt;

// ReSharper disable UnusedMember.Global

// ReSharper disable CheckNamespace
public static class XSqlParameterExtensions
// ReSharper restore CheckNamespace
{
    public static SqlParameter AsOutput(this SqlParameter parameter)
    {
        parameter.Direction = ParameterDirection.Output;

        return parameter;
    }

    public static SqlParameter AsReturnValue(this SqlParameter parameter)
    {
        parameter.Direction = ParameterDirection.ReturnValue;

        return parameter;
    }

    public static SqlXml AsSqlXml(this string content)
    {
        return new SqlXml(value: XmlReader.Create(input: new StringReader(content)));
    }

    public static SqlCommand AddParameters(this SqlCommand cmd, params SqlParameter[] args)
    {
        cmd.Parameters.AddRange(args);

        return cmd;
    }

    public static SqlParameterCollection AddParameters(this SqlParameterCollection parameters, params SqlParameter[] args)
    {
        parameters.AddRange(args);

        return parameters;
    }

    public static SqlParameter Set(this SqlParameter parameter, string name, SqlDbType type, object value, bool setDbNullOnNullValue = true)
    {
        parameter.ParameterName = name;
        parameter.SqlDbType = type;
        parameter.Value = setDbNullOnNullValue && Equals(value, objB: null) ? DBNull.Value : value;

        return parameter;
    }

    public static SqlParameter SetAsUdt(this SqlParameter parameter, string name, object value)
    {
        if (value is DataTable table)
        {
            parameter.Set(name, SqlDbType.Structured, value: table);
            parameter.TypeName = table.TableName;
        }
        else if (value is UdtBase udtBase)
        {
            parameter.Set(name, SqlDbType.Structured, value: udtBase?.Table);
            parameter.TypeName = udtBase?.Table.TableName;
        }
        else
        {
            parameter.Set(name, SqlDbType.Udt, value: value);
            parameter.TypeName = $"dbo.{value.GetType().Name}";
        }

        return parameter;
    }

    public static SqlParameter SetAsXml(this SqlParameter parameter, string name, string value)
    {
        parameter.Set(name, SqlDbType.Xml, value.AsSqlXml());

        return parameter;
    }
}