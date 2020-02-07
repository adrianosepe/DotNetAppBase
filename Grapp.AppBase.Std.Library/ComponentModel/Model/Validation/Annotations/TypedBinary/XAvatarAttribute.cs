using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedBinary
{
	public class XAvatarAttribute : XImageAttribute
	{
		public const long DefaultNormalSize = XHelper.UnitOfMeasure.Megabyte * 2;

		public XAvatarAttribute() : base(DefaultNormalSize) { }
	}
}