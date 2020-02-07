using System;
using System.ComponentModel;
using System.Linq;

namespace Grapp.AppBase.Std.Exceptions.Base
{
    public class XInvalidOperationException : XException
    {
        public XInvalidOperationException() { }

        public XInvalidOperationException([Localizable(isLocalizable: false)] string message) : base(message) { }

        public XInvalidOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}