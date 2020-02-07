using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Grapp.AppBase.Std.Library.Dynamic
{
	public class ExpanderObject : DynamicObject
	{
		public Dictionary<string, object> ObjectDictionary;

		public ExpanderObject()
		{
			ObjectDictionary = new Dictionary<string, object>();
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			object val;
			if(ObjectDictionary.TryGetValue(binder.Name, out val))
			{
				result = val;
				return true;
			}

			result = null;
			return true;
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			try
			{
				ObjectDictionary[binder.Name] = value;
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}
	}
}