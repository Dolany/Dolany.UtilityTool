using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dolany.UtilityTool
{
    public static class LinqExtention
    {
        /// <summary>
        /// 元素集合是否为Null或者空
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="objs">元素集合</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> objs)
        {
            return objs == null || !objs.Any();
        }

        /// <summary>
        /// 交换IList中两个元素的位置
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="array">元素列表</param>
        /// <param name="firstIdx">第一个元素的索引</param>
        /// <param name="secondIdx">第二个元素的索引</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static void Swap<T>(this IList<T> array, int firstIdx, int secondIdx)
        {
            if (array.IsNullOrEmpty())
            {
                return;
            }

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
            if (dic.IsNullOrEmpty())
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

        /// <summary>
        /// 根据表达式去除列表中的某些项目
        /// </summary>
        /// <typeparam name="T">指定元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="valueExpression">判断表达式（符合表达式的元素将会被移除）</param>
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

        /// <summary>
        /// 用指定的字符串连接字符串集合
        /// </summary>
        /// <param name="strs">字符串集合</param>
        /// <param name="spliter">连接字符串</param>
        /// <returns>连接后的字符串</returns>
        /// <exception cref="OutOfMemoryException"></exception>
        public static string JoinToString(this IEnumerable<string> strs, string spliter)
        {
            return strs.IsNullOrEmpty() ? null : string.Join(spliter, strs);
        }
    }
}
