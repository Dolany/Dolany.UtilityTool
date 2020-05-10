using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dolany.UtilityTool.Linq
{
    public static class LinqExtention
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> objs)
        {
            return objs == null || !objs.Any();
        }

        public static void Swap<T>(this IList<T> array, int firstIdx, int secondIdx)
        {
            var temp = array[firstIdx];
            array[firstIdx] = array[secondIdx];
            array[secondIdx] = temp;
        }

        /// <summary>
        /// 根据表达式去除字典中的某些项目
        /// </summary>
        /// <typeparam name="TKey">Key类型</typeparam>
        /// <typeparam name="TValue">Value类型</typeparam>
        /// <param name="dic">字典</param>
        /// <param name="valueExpression">Value判定表达式</param>
        public static void Remove<TKey, TValue>(this Dictionary<TKey, TValue> dic, Expression<Predicate<TValue>> valueExpression)
        {
            if (dic == null || !dic.Any())
            {
                return;
            }

            var check = valueExpression.Compile();
            for (var i = 0; i < dic.Keys.Count; i++)
            {
                var key = dic.Keys.ElementAt(i);
                if (!check(dic[key]))
                {
                    continue;
                }

                dic.Remove(key);
                i--;
            }
        }

        public static void Remove<T>(this IList<T> list, Expression<Predicate<T>> valueExpression)
        {
            if (list.IsNullOrEmpty())
            {
                return;
            }

            var check = valueExpression.Compile();
            for (var i = 0; i < list.Count; i++)
            {
                if (!check(list[i]))
                {
                    continue;
                }

                list.RemoveAt(i);
                i--;
            }
        }

        public static string JoinToString(this IEnumerable<string> strs, string spliter)
        {
            return string.Join(spliter, strs);
        }
    }
}
