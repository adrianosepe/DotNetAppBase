using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Grapp.AppBase.Std.Exceptions.Internals
{
	internal class XHelper
	{
		public class Resources
		{
			public static byte[] LoadResource(Assembly asm, string resourceName)
			{
				using(var stream = new MemoryStream())
				{
					asm.GetManifestResourceStream(name: $"{asm.GetName().Name}.{resourceName}")?.CopyTo(stream);

					return stream.ToArray();
				}
			}

            public static byte[] LoadResource(object context, string resourceName) => LoadResource(context.GetType().Assembly, resourceName);
        }
	}
}