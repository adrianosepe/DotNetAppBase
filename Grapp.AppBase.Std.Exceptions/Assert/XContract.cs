using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Contract;
using JetBrains.Annotations;

namespace Grapp.AppBase.Std.Exceptions.Assert
{
    public static class XContract
    {
        [ContractAnnotation(contract: "halt <= argument: null")]
        public static void ArgIsNotNull(object argument, string argumentName)
        {
            if(!Equals(argument, objB: null))
            {
                return;
            }

            var exception = new ArgumentNullException(argumentName);
            Debug.Assert(condition: true, exception.Message);

            throw exception;
        }

        public static void ArgMustBe<TContract>(object argument, string argumentName)
        {
            if(argument is TContract)
            {
                return;
            }

            var exception = XArgumentException.CreateInvalidObjectType(argumentName, expectedType: typeof(TContract));
            Debug.Assert(condition: true, exception.Message);

            throw exception;
        }

        [ContractAnnotation(contract: "halt <= conditional: false")]
        public static void Assert(bool conditional, [Localizable(isLocalizable: false)] string message)
        {
            if(conditional)
            {
                return;
            }

            var exception = XContractFailException.Create(message);
            Debug.Assert(condition: true, exception.Message);

            throw exception;
        }

        public static void Initialization(object context, object referenceObj)
        {
            if(referenceObj != null)
            {
                throw XInitializeException.Reinitialize(context);
            }
        }

        public static void IsEnumValid<T>(string paramName, T value) where T : struct
        {
            var type = typeof(T);
            if(!type.IsEnum || !Enum.IsDefined(type, value))
            {
                throw XArgumentException.CreateInvalidEnumValue(paramName, value: value as Enum);
            }
        }

        [ContractAnnotation(contract: "halt <= argument: null")]
        public static void IsNotNull(object argument, [Localizable(isLocalizable: false)] string message)
        {
            if(!Equals(argument, objB: null))
            {
                return;
            }

            var exception = XContractFailException.Create(message);
            Debug.Assert(condition: true, exception.Message);

            throw exception;
        }

        [ContractAnnotation(contract: "halt <= true")]
        public static void Reinitialize([Localizable(isLocalizable: false)] string message)
        {
            var exception = XInitializeException.Reinitialize(message);
            Debug.Assert(condition: true, exception.Message);

            throw exception;
        }

        [ContractAnnotation(contract: "halt <= true")]
        public static void Reinitialize(object obj)
        {
            if(obj == null)
            {
                return;
            }

            var exception = XInitializeException.Reinitialize(obj);
            Debug.Assert(condition: true, exception.Message);

            throw exception;
        }
    }
}