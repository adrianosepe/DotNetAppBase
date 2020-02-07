using System;
using System.ComponentModel;
using System.Linq;

namespace Grapp.AppBase.Std.Exceptions.Contract
{
    [Serializable]
    public class XArgumentOutOfRangeException : ArgumentOutOfRangeException
    {
        protected XArgumentOutOfRangeException(string paramName) : base(paramName) { }

        protected XArgumentOutOfRangeException(string paramName, string message, object actualValue) : base(paramName, actualValue, message) { }

        public static XArgumentOutOfRangeException Create(string paramName) => new XArgumentOutOfRangeException(paramName);

        public static XArgumentOutOfRangeException Create(string paramName, [Localizable(isLocalizable: false)] string message) => new XArgumentOutOfRangeException(paramName, message, actualValue: null);

        public static XArgumentOutOfRangeException Create(string paramName, object actualValue, [Localizable(isLocalizable: false)] string message) => new XArgumentOutOfRangeException(paramName, message, actualValue);
    }
}