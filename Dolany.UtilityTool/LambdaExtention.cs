using System;
using System.Threading;

namespace Dolany.UtilityTool
{
    public static class LambdaExtention
    {
        /// <summary>
        /// 按指定时间间隔重试方法
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="RetryFunc">需要重试的函数</param>
        /// <param name="RetryIntervals">重试时间间隔</param>
        /// <param name="ResulteChecker">结果判别式，结果如果不符合判别式将会被判定为失败</param>
        /// <returns>函数执行结果，如果多次尝试仍失败，则返回默认值</returns>
        public static TResult DoWithRetry<TResult>(Func<TResult> RetryFunc, TimeSpan[] RetryIntervals, Predicate<TResult> ResulteChecker = null)
        {
            TResult result;
            var retryCount = 0;
            do
            {
                if (DoWithRetry(RetryFunc, out result, ResulteChecker))
                {
                    return result;
                }

                if (retryCount >= RetryIntervals.Length)
                {
                    break;
                }

                Thread.Sleep(RetryIntervals[retryCount]);
                retryCount++;
            } while (true);

            return result;
        }

        private static bool DoWithRetry<TResult>(Func<TResult> RetryFunc, out TResult Result, Predicate<TResult> ResulteChecker = null)
        {
            Result = default;
            try
            {
                Result = RetryFunc();
                return ResulteChecker == null || ResulteChecker(Result);
            }
            catch
            {
                return false;
            }
        }
    }
}
