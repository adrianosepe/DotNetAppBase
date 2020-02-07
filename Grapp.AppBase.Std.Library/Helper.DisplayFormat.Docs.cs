using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation;

namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public static partial class DisplayFormat
		{
			public static class Docs
			{
				public static string Cnpj(string value)
				{
					return Mask.Format(ValidationDataFormats.DocCnpjMask, value);
				}
			}
		}
	}
}