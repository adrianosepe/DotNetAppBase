using System;
using System.ComponentModel;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Base;

namespace Grapp.AppBase.Std.Exceptions.Contract
{
	public class XInitializeException : XException
	{
		[Localizable(isLocalizable: false)]
		protected XInitializeException(string message) : base(message) { }

		[Localizable(isLocalizable: false)]
		public static XInitializeException Create(string message) => new XInitializeException(message);

	    public static XInitializeException CreateForNullReference(object initializingOject, string requireMemberName)
	        => new XInitializeException(
	            message: $"The type {initializingOject.GetType().Name} is initialized, but the member '{requireMemberName}' is required and NULL!");

	    public static XInitializeException InvalidInheritance(object initializingOject, string inheritanceMethodName)
	        => new XInitializeException(
	            message: $"The type {initializingOject.GetType().Name} don't supports the initialization from this member '{inheritanceMethodName}', try use the overloaded!");

	    public static XInitializeException PropertyButRequireMemberIsNull(object initializingPropertyOf, string propertyName, string requireMemberName)
	        => new XInitializeException(
	            message: $"The type {initializingPropertyOf.GetType().Name} had a property {propertyName} initialized, but the member '{requireMemberName}' is required and NULL!");

	    public static XInitializeException Reinitialize(string message) => new XInitializeException(message);

	    public static XInitializeException Reinitialize(object obj) => new XInitializeException(message: $"The type {obj.GetType().Name} is initialized, you can't do this again!");
	}
}