using System.IO;
using System.Reflection;

namespace Grapp.AppBase.Std.Library
{
	public partial class XHelper
	{
		public class Resources
		{
			public static byte[] LoadResource(Assembly asm, string resourceName)
			{
				using(var stream = new MemoryStream())
				{
					asm.GetManifestResourceStream($"{asm.GetName().Name}.{resourceName}")?.CopyTo(stream);

					return stream.ToArray();
				}
			}

            public static byte[] LoadResource(object context, string resourceName)
		    {
		        return LoadResource(context.GetType().Assembly, resourceName);
		    }
        }
	}
}