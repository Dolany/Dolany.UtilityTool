using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolany.UtilityTool
{
    /// <summary>
    /// 对象扩展方法
    /// </summary>
    public static class ObjExtention
    {
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

        /// <summary>
        /// 深度克隆
        /// </summary>
        /// <typeparam name="T">克隆对象类型</typeparam>
        /// <param name="obj">待克隆的对象</param>
        /// <returns>克隆体</returns>
        public static T Clone<T>(this T obj) where T : class, new()
        {
            var type = obj.GetType();
            var copyT = new T();
            foreach (var prop in type.GetProperties())
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    prop.SetValue(copyT, prop.GetValue(obj));
                }
            }

            return copyT;
        }

        /// <summary>
        /// 获取异常的详细信息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <returns>异常的详细信息</returns>
        public static string GetFullDetailMsg(this Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }

            var msg = ex.Message + ex.StackTrace;
            while (ex.InnerException != null)
            {
                msg += $"\r\nInner Exception:{ex.InnerException.Message} {ex.InnerException.StackTrace}";

                ex = ex.InnerException;
            }

            return msg;
        }
    }
}
