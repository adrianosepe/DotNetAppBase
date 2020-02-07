namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public static class Arrays
		{
			public static bool IsEmpty<T>(T[] array)
			{
				return array.Length == 0;
			}

			public static bool IsNullOrEmpty<T>(T[] array)
			{
				return array == null || array.Length == 0;
			}

		    public static T[] Create<T>(params T[] args)
		    {
		        return args;
		    }
		}
	}
}