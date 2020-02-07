using System;
using System.Linq;
using System.Xml.Serialization;
using Grapp.AppBase.Std.Library;

// ReSharper disable CheckNamespace
public static partial class XObjectExtensions
// ReSharper restore CheckNamespace
{
	public static object NotNull(this object obj)
	{
		if(Equals(obj, objB: null))
		{
			return null;
		}

		var type = obj.GetType();
		if(type.IsEnum)
		{
			var memberInfo = XHelper.Reflections.Fields.GetStatic(type, fieldName: obj.ToString());
			var xmlEnumMember = XHelper.Reflections.Attributes.Get<XmlEnumAttribute>(memberInfo);

			if(xmlEnumMember != null)
			{
				return xmlEnumMember.Name;
			}
		}

		return obj.ToString();
	}
}