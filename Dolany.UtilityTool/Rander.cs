using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Dolany.UtilityTool
{
    public static class Rander
    {
        private static readonly RNGCryptoServiceProvider RngCsp = new RNGCryptoServiceProvider();

        /// <summary>
        /// 获取一个随机整数
        /// </summary>
        /// <param name="MaxValue">最大值（不包含）</param>
        /// <returns>0到最大值之间的一个随机整数</returns>
        public static int RandInt(int MaxValue)
        {
            if (MaxValue == 0)
            {
                return 0;
            }

            var bytes = new byte[4];
            RngCsp.GetBytes(bytes);

            var value = BitConverter.ToInt32(bytes, 0);
            var rand = new Random(value);
            return rand.Next(0, MaxValue);
        }

        /// <summary>
        /// 获取区间范围内的一个随机整数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值（不包含）</param>
        /// <returns>区间范围内的一个随机整数</returns>
        public static int RandRange(int minValue, int maxValue)
        {
            if (minValue >= maxValue)
            {
                return minValue;
            }

            return minValue + RandInt(maxValue - minValue + 1);
        }

        /// <summary>
        /// 获取一个随机的布尔值
        /// </summary>
        /// <returns>随机的布尔值</returns>
        public static bool RandBool()
        {
            return RandInt(2) == 0;
        }

        /// <summary>
        /// 获取集合中的一个随机元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T RandElement<T>(this IEnumerable<T> collection)
        {
            var list = collection?.ToList();
            if (list == null || list.IsNullOrEmpty())
            {
                return default;
            }

            var length = list.Count;
            var idx = RandInt(length);
            return list.ElementAt(idx);
        }

        /// <summary>
        /// 将一个数组随机排列
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>重新排列后的数组</returns>
        public static T[] RandSort<T>(T[] array)
        {
            if (array.IsNullOrEmpty())
            {
                return new T[0];
            }

            var result = array.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                var randIdx = RandInt(array.Length - i) + i;

                result.Swap(i, randIdx);
            }

            return result;
        }

        /// <summary>
        /// 按几率字典随机选取一个随机元素
        /// </summary>
        /// <typeparam name="TKey">元素类型</typeparam>
        /// <param name="ratedDic">几率字典</param>
        /// <returns>随机元素</returns>
        public static TKey RandRated<TKey>(this Dictionary<TKey, int> ratedDic)
        {
            if (ratedDic.IsNullOrEmpty())
            {
                return default;
            }

            var sumRate = ratedDic.Sum(p => p.Value);
            var randIdx = RandInt(sumRate);

            var rSum = 0;
            foreach (var (key, value) in ratedDic)
            {
                rSum += value;
                if (rSum >= randIdx)
                {
                    return key;
                }
            }

            return default;
        }

        /// <summary>
        /// 按几率字典随机选取若干个不同的元素
        /// </summary>
        /// <typeparam name="TKey">元素类型</typeparam>
        /// <param name="ratedDic">几率字典</param>
        /// <param name="count">需要选取的个数</param>
        /// <returns>选取结果</returns>
        public static List<TKey> RandRated<TKey>(this Dictionary<TKey, int> ratedDic, int count)
        {
            if (ratedDic.IsNullOrEmpty() || count <= 0)
            {
                return new List<TKey>();
            }

            var resultList = new List<TKey>();
            var realCount = Math.Min(count, ratedDic.Count);
            var dic = ratedDic.ToDictionary(p => p.Key, p => p.Value);
            for (var i = 0; i < realCount; i++)
            {
                var randEle = dic.RandRated();
                resultList.Add(randEle);
                dic.Remove(randEle);
            }

            return resultList;
        }
    }
}
