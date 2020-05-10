using System;
using System.Threading;

namespace Dolany.UtilityTool.Lambda
{
    public static class LambdaExtention
    {
        public static TResult DoWithRetry<TResult>(this Func<TResult> RetryFunc, TimeSpan[] RetryIntervals, Predicate<TResult> ResulteChecker = null)
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

        private static bool DoWithRetry<TResult>(this Func<TResult> RetryFunc, out TResult Result, Predicate<TResult> ResulteChecker = null)
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
