using DotNetAppBase.Std.Library.ComponentModel.Model.Validation;

namespace DotNetAppBase.Std.Library
{
	public partial class XHelper
	{
		public static partial class DisplayFormat
		{
			public static class Docs
			{
				public static string Cnpj(string value) => Mask.Format(ValidationDataFormats.DocCnpjMask, value);
            }
		}
	}
}