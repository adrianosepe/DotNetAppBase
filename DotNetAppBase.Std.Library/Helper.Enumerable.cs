using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DotNetAppBase.Std.Exceptions.Assert;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Library
{
    public partial class XHelper
    {
        public static class Enumerable
        {
            public static T[] ToArrayEfficient<T>(IEnumerable<T> values)
            {
                var array = values as T[];
                return array ?? values.ToArray();
            }

            public static List<T> ToListEfficient<T>(IEnumerable<T> values)
            {
                var array = values as List<T>;

                return array ?? values.ToList();
            }

            public static bool FirstOrDefault<T>(IEnumerable<T> values, out T value)
            {
                value = values.FirstOrDefault();

                return !EqualityComparer<T>.Default.Equals(value, default);
            }

            public static BindingList<T> ToBindingList<T>(IEnumerable<T> data) where T : class
            {
                var bl = data as BindingList<T>;

                return bl ?? new BindingList<T>(ToListEfficient(data));
            }

            public static void ForEach<T>(IEnumerable<T> enumerable, Action<T> action)
            {
                XContract.ArgIsNotNull(enumerable, nameof(enumerable));
                XContract.ArgIsNotNull(action, nameof(action));

                foreach (var item in enumerable)
                {
                    action(item);
                }
            }

            public static bool IsIn<T>(T value, params T[] args)
            {
                if (Obj.IsNull(value))
                {
                    return false;
                }

                return args.Any(arg => arg?.Equals(value) ?? false);
            }

            public static bool IsNullOrEmpty<T>(IEnumerable<T> enumerable)
            {
                if (enumerable == null)
                {
                    return true;
                }

                if (enumerable is T[] asArray)
                {
                    return asArray.Length == 0;
                }

                return !enumerable.Any();
            }

            public static TSource MinBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                if (selector == null)
                {
                    throw new ArgumentNullException(nameof(selector));
                }

                if (comparer == null)
                {
                    throw new ArgumentNullException(nameof(comparer));
                }

                using var sourceIterator = source.GetEnumerator();
                if (!sourceIterator.MoveNext())
                {
                    throw new XInvalidOperationException("Sequence was empty");
                }

                var min = sourceIterator.Current;
                var minKey = selector(min);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, minKey) < 0)
                    {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }

                return min;
            }

            public static IEnumerable<T> Subtraction<T>(IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, Comparison<T> compare = null)
            {
                XContract.ArgIsNotNull(enumerable1, nameof(enumerable1));
                XContract.ArgIsNotNull(enumerable2, nameof(enumerable2));

                if (compare == null)
                {
                    compare = (x, y) => Obj.AreEquals(x, y) ? 0 : 1;
                }

                enumerable1 = enumerable1.ToList();

                if (!enumerable1.Any())
                {
                    return new T[0];
                }

                enumerable2 = enumerable2.ToList();

                var result = new List<T>();

                ForEach(
                    enumerable1,
                    c =>
                        {
                            // ReSharper disable SimplifyLinqExpression
                            if (!enumerable2.Any(x => compare(c, x) == 0))
                                // ReSharper restore SimplifyLinqExpression
                            {
                                result.Add(c);
                            }
                        });

                return result.ToArray();
            }
        }
    }
}