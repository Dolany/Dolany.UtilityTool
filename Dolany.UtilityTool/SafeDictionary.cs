using System.Collections.Generic;

namespace Dolany.UtilityTool
{
    /// <summary>
    /// 安全字典
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    public class SafeDictionary<TKey, TValue>
    {
        /// <summary>
        /// 被封装的字典数据
        /// </summary>
        public Dictionary<TKey, TValue> Data;

        /// <summary>
        /// 根据字典初始化
        /// </summary>
        /// <param name="data"></param>
        public SafeDictionary(Dictionary<TKey, TValue> data)
        {
            Data = data;
        }

        /// <summary>
        /// 字典数据是否为空
        /// </summary>
        public bool IsEmpty => Data.IsNullOrEmpty();

        /// <summary>
        /// 空的安全字典
        /// </summary>
        public static SafeDictionary<TKey, TValue> Empty => new SafeDictionary<TKey, TValue>(new Dictionary<TKey, TValue>());

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get => Data.GetDicValueSafe(key);
            set => Add(key, value);
        }

        /// <summary>
        /// 添加键值对到字典中，如果已经存在键，则更新其值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Add(TKey key, TValue value)
        {
            Data ??= new Dictionary<TKey, TValue>();

            if (key == null)
            {
                return;
            }

            Data.AddSafe(key, value);
        }

        /// <summary>
        /// 根据键获取long类型的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public long GetLong(TKey key)
        {
            return this[key].ToLongSafe();
        }

        /// <summary>
        /// 根据键获取int类型的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public int GetInt(TKey key)
        {
            return this[key].ToIntSafe();
        }

        /// <summary>
        /// 根据键获取double类型的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public double GetDouble(TKey key)
        {
            return this[key].ToDoubleSafe();
        }

        /// <summary>
        /// 根据键获取string类型的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public string GetString(TKey key)
        {
            return this[key].ToStringSafe();
        }

        /// <summary>
        /// 根据键获取指定类型的值
        /// </summary>
        /// <typeparam name="TAimType">目标类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public TAimType Get<TAimType>(TKey key) where TAimType: class
        {
            return this[key] as TAimType;
        }
    }
}
