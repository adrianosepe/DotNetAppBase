using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Key
{
	public class XMasterKeyAttribute : XRequiredAttribute
	{
	    public override bool Enabled => false;
	}
}