using System;
using System.Linq;
using Grapp.AppBase.Std.Library;

// ReSharper disable CheckNamespace
public static partial class XObjectExtensions
// ReSharper restore CheckNamespace
{
	public static string AsJson(this object obj)
	{
		if(Equals(obj, objB: null))
		{
			return NullDefaultDisplay;
		}

		return XHelper.Serializers.Json.Serialize(obj);
	}

	public static string AsXml(this object obj)
	{
		if(Equals(obj, objB: null))
		{
			return NullDefaultDisplay;
		}

		return XHelper.Serializers.Xml.Serialize(obj);
	}

	public static string AsXmlDataContract(this object obj)
	{
		if(Equals(obj, objB: null))
		{
			return NullDefaultDisplay;
		}

		return XHelper.Serializers.Xml.SerializeDataContract(obj);
	}
}