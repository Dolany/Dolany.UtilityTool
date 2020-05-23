using System.Collections.Generic;
using System.Linq;

namespace Dolany.UtilityTool
{
    public static class SafeConvert
    {
        /// <summary>
        /// 安全的获取一个字典中的键值
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="Dic">源字典</param>
        /// <param name="key">键</param>
        /// <returns>键对应的值</returns>
        public static TValue GetDicValueSafe<TKey, TValue>(this Dictionary<TKey, TValue> Dic, TKey key)
        {
            return Dic != null && key != null && Dic.ContainsKey(key) && Dic.TryGetValue(key, out var result) ? result : default;
        }

        /// <summary>
        /// 将字典转化为安全字典
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <typeparam name="TValue">字典值类型</typeparam>
        /// <param name="Dic">源字典</param>
        /// <returns>封装的安全字典</returns>
        public static SafeDictionary<TKey, TValue> ToSafe<TKey, TValue>(this Dictionary<TKey, TValue> Dic)
        {
            return new SafeDictionary<TKey, TValue>(Dic);
        }

        /// <summary>
        /// 将对象安全的转化为string类型
        /// </summary>
        /// <param name="obj">源数据</param>
        /// <returns>string类型的数值</returns>
        public static string ToStringSafe(this object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        /// <summary>
        /// 将对象安全的转化为Int类型
        /// </summary>
        /// <param name="obj">源数据</param>
        /// <returns>Int类型的数值</returns>
        public static int ToIntSafe(this object obj)
        {
            return int.TryParse(obj.ToStringSafe(), out var result) ? result : 0;
        }

        /// <summary>
        /// 将对象安全的转化为Double类型
        /// </summary>
        /// <param name="obj">源数据</param>
        /// <returns>Double类型的数值</returns>
        public static double ToDoubleSafe(this object obj)
        {
            return double.TryParse(obj.ToStringSafe(), out var result) ? result : 0;
        }

        /// <summary>
        /// 将对象安全的转化为Long类型
        /// </summary>
        /// <param name="obj">源数据</param>
        /// <returns>Long类型的数值</returns>
        public static long ToLongSafe(this object obj)
        {
            return long.TryParse(obj.ToStringSafe(), out var result) ? result : 0;
        }

        /// <summary>
        /// 安全的向字典中添加键和值
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dic">目标字典</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void AddSafe<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            dic ??= new Dictionary<TKey, TValue>();

            if (key == null)
            {
                return;
            }

            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.TryAdd(key, value);
            }
        }

        /// <summary>
        /// 将对象转换为字典
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="obj">对象数据</param>
        /// <returns>数据字典</returns>
        public static Dictionary<string, object> ObjectToDictionary<T>(this T obj)
        {
            if (obj == null)
            {
                return null;
            }

            var type = obj.GetType();
            var safeDic = SafeDictionary<string, object>.Empty;
            foreach (var propertyInfo in type.GetProperties().Where(p => p.CanRead))
            {
                safeDic[propertyInfo.Name] = propertyInfo.GetValue(obj);
            }

            return safeDic.Data;
        }

        /// <summary>
        /// 将数据字典转换为指定类型
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="dic">数据字典</param>
        /// <returns>对象数据</returns>
        public static T DictionaryToObject<T>(this Dictionary<string, object> dic) where T : new()
        {
            if (dic.IsNullOrEmpty())
            {
                return default;
            }

            var type = typeof(T);
            var result = new T();
            foreach (var propertyInfo in type.GetProperties().Where(p => p.CanWrite))
            {
                if (!dic.ContainsKey(propertyInfo.Name))
                {
                    continue;
                }

                propertyInfo.SetValue(result, dic[propertyInfo.Name]);
            }

            return result;
        }
    }
}
