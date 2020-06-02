using System;
using System.Collections.Generic;

namespace Dolany.UtilityTool
{
    /// <summary>
    /// 安全转换的方法
    /// </summary>
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
            return obj switch
            {
                null => string.Empty,
                DateTime time => time.ToLocalTime().ToString("yyyy/m/dd HH:mm:ss"),
                _ => obj.ToString()
            };
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
        /// 将对象安全的转换为DateTime类型（转为本地时间）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeSafe_Local(this object obj)
        {
            if (obj == null)
            {
                return DateTime.Now;
            }

            try
            {
                return obj is DateTime ? Convert.ToDateTime(obj).ToLocalTime() : Convert.ToDateTime(obj);
            }
            catch (Exception)
            {
                var str = obj.ToStringSafe().Substring(0, 19);
                DateTime date;
                return DateTime.TryParse(str, out date) ? date : DateTime.Now;
            }
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
    }
}
