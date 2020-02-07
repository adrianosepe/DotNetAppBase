using System;
using System.ComponentModel;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Assert;
using Grapp.AppBase.Std.Exceptions.Base;

namespace Grapp.AppBase.Std.Exceptions.Contract
{
	public class XArgumentTypeException : XException
	{
		private const string DefaultMessage = "The argument {0} is incompatible with expected type!\n\nWait.Type: {1}\nReceive.Type: {2}";

		protected XArgumentTypeException(string argumentName, Type wait, Type received = null)
			: base(message: String.Format(DefaultMessage, argumentName, wait.FullName, arg2: GetTypeName(received)))
		{
			XContract.ArgIsNotNull(argumentName, argumentName: nameof(argumentName));
			XContract.ArgIsNotNull(wait, argumentName: nameof(wait));
			XContract.ArgIsNotNull(received, argumentName: nameof(received));

			ArgumentName = argumentName;
			Wait = wait;
			Received = received;
		}

		public string ArgumentName { get; }

		public Type Wait { get; }

		public Type Received { get; }

		public static XArgumentTypeException Create(string argumentName, Type wait)
		{
			if(argumentName == null)
			{
				throw new ArgumentNullException(paramName: nameof(argumentName));
			}

			if(wait == null)
			{
				throw new ArgumentNullException(paramName: nameof(wait));
			}

			return new XArgumentTypeException(argumentName, wait, received: typeof(NullReference));
		}

		public static XArgumentTypeException CreateFromObject([Localizable(isLocalizable: false)] string argumentName, Type wait, object received)
		{
			if(argumentName == null)
			{
				throw new ArgumentNullException(paramName: nameof(argumentName));
			}

			if(wait == null)
			{
				throw new ArgumentNullException(paramName: nameof(wait));
			}

			return new XArgumentTypeException(argumentName, wait, received: received?.GetType() ?? typeof(NullReference));
		}

		public static XArgumentTypeException CreateFromType(string argumentName, Type wait, Type received)
		{
			if(argumentName == null)
			{
				throw new ArgumentNullException(paramName: nameof(argumentName));
			}
			if(wait == null)
			{
				throw new ArgumentNullException(paramName: nameof(wait));
			}
			if(received == null)
			{
				throw new ArgumentNullException(paramName: nameof(received));
			}

			return new XArgumentTypeException(argumentName, wait, received);
		}

		private static string GetTypeName(Type type)
		{
		    // ReSharper disable LocalizableElement
		    return type == typeof(NullReference) ? "[Object is NULL]" : type.FullName;
		    // ReSharper restore LocalizableElement
		}

		private static class NullReference { }
	}
}