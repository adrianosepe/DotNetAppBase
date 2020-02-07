using System;
using System.Linq;

// ReSharper disable CheckNamespace
namespace Grapp.AppBase.Std.ComponentModel.Model.Extensions {
    public static class EntityBaseExtensions
// ReSharper restore CheckNamespace
    {
        public static TResult ReadNavigateDataIfNotNull<TModel, TResult>(
            this TModel model, Func<TModel, bool> canReadFunc, Func<TModel, TResult> readFunc, TResult defaultValue)
            where TModel : class
        {
            return canReadFunc(model) ? readFunc(model) : defaultValue;
        }

        public static TResult ReadNavigateDataIfNotNull<TModel, TNaviProperty, TResult>(
            this TModel model, TNaviProperty property, Func<TNaviProperty, TResult> readFunc, TResult defaultValue = default)
            where TModel : class
            where TNaviProperty : class
        {
            return property != null ? readFunc(property) : defaultValue;
        }
    }
}
