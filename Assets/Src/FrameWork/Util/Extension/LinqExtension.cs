using System;
using System.Collections.Generic;

namespace Util.Extension
{
    public static class LinqExtension
    {
        public static void ForEachEx<T>(this IEnumerable<T> items,Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static int CountEx<T>(this IEnumerable<T> items,Func<T,bool> condition)
        {
            var n = 0;
            foreach (var item in items)
            {
                if (condition(item))
                {
                    n++;
                }
            }

            return n;
        }

        public static IEnumerable<TResult> SelectEx<TSource,TResult>(this IEnumerable<TSource> items, Func<TSource, TResult> selector)
        {
            foreach (var item in items)
            {
                yield return selector(item);
            }
        }
        
        public static IEnumerable<TSource> WhereEx<TSource>(this IEnumerable<TSource> items, Func<TSource, bool> condition)
        {
            foreach (var item in items)
            {
                if (condition(item))
                {
                    yield return item;
                }
            }
        }

        public static void RemoveBy<T>(this List<T> list,Func<T,bool> condition)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var t = list[i];
                if (condition(t))
                {
                    list.RemoveAt(i);
                }
            }
        }
        
        public static void RemoveSingle<T>(this List<T> list,Func<T,bool> condition)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var t = list[i];
                if (condition(t))
                {
                    list.RemoveAt(i);
                    break;
                }
            }
        }
    }
}