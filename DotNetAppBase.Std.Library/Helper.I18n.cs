using System;
using System.Globalization;
using System.Threading;
using DotNetAppBase.Std.Exceptions.Assert;

namespace DotNetAppBase.Std.Library
{
    public partial class XHelper
    {
        // ReSharper disable InconsistentNaming
        public static class I18n
            // ReSharper restore InconsistentNaming
        {
            private static CultureInfo _currentCulture;
            private static ELanguage _currentLanguage;

            static I18n()
            {
                _currentLanguage = ELanguage.Invariant;

                InternalCurrentLanguageChanged();
            }

            public static CultureInfo CurrentCulture => _currentCulture;

            public static ELanguage CurrentLanguage
            {
                get => _currentLanguage;
                set
                {
                    if(_currentLanguage != value)
                    {
                        XContract.IsEnumValid(nameof(CurrentLanguage), value);

                        _currentLanguage = value;

                        InternalCurrentLanguageChanged();

                        CurrentLanguageChanged?.Invoke(null, EventArgs.Empty);
                    }
                }
            }

            public static string NumberDecimalSeparator => CurrentCulture.NumberFormat.NumberDecimalSeparator;

            public static event EventHandler CurrentLanguageChanged;

            private static CultureInfo IdentifyCulture(ELanguage currentLanguage)
            {
                switch(currentLanguage)
                {
                    case ELanguage.Brazil:
                        return new CultureInfo("pt-BR");

                    case ELanguage.Paraguay:
                        return new CultureInfo("es-PY");

                    default:
                        return CultureInfo.InvariantCulture;
                }
            }

            private static void InternalCurrentLanguageChanged()
            {
                _currentCulture = IdentifyCulture(_currentLanguage);

                Thread.CurrentThread.CurrentCulture = _currentCulture;
                Thread.CurrentThread.CurrentUICulture = _currentCulture;
            }

            public enum ELanguage
            {
                Invariant = 0,
                Brazil = 1,
                Paraguay = 2
            }
        }
    }
}