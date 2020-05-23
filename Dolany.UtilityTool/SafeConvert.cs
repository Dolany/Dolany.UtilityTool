using System.Collections.Generic;
using System.Linq;

namespace Dolany.UtilityTool
{
    public static class SafeConvert
    {
        public static TValue GetDicValueSafe<TKey, TValue>(this Dictionary<TKey, TValue> Dic, TKey key)
        {
            return Dic != null && key != null && Dic.ContainsKey(key) && Dic.TryGetValue(key, out var result) ? result : default;
        }

        public static SafeDictionary<TKey, TValue> ToSafe<TKey, TValue>(this Dictionary<TKey, TValue> Dic)
        {
            return new SafeDictionary<TKey, TValue>(Dic);
        }

        public static string ToStringSafe(this object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        public static int ToIntSafe(this object obj)
        {
            return int.TryParse(obj.ToStringSafe(), out var result) ? result : 0;
        }

        public static double ToDoubleSafe(this object obj)
        {
            return double.TryParse(obj.ToStringSafe(), out var result) ? result : 0;
        }

        public static long ToLongSafe(this object obj)
        {
            return long.TryParse(obj.ToStringSafe(), out var result) ? result : 0;
        }

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
