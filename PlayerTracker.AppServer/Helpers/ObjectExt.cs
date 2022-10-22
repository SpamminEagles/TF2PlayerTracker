using System;

namespace PlayerTracker.AppServer.Helpers
{
    public static class ObjectExt
    {

        /// <summary>
        /// Exposes an onbjecz to a lambda in its parameters, allowing for
        /// chaining return values into another expression.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the parameter expected by the lambda
        /// </typeparam>
        /// <typeparam name="U">Return value of the lambda</typeparam>
        /// <param name="arg">Extended value</param>
        /// <param name="func">Lambda to be executed</param>
        /// <returns></returns>
        public static U Chain<T, U>(this T arg, Func<T, U> func) => func(arg);
        public static void Chain<T>(this T arg, Action<T> func) => func(arg);


        //public static void WithValue<T>(Func<T> gen, Action<T> use) => use(gen());

        public static (T1, T2) WithAnother<T1, T2>(this T1 _1, T2 _2) => (_1, _2);
        public static (T1, T2, T3) WithAnother<T1, T2, T3>(this (T1 _1, T2 _2) t, T3 _3) => (t._1, t._2, _3);

    }
}
