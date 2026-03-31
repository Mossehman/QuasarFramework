using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuasarFramework.Lerp {

    /// <summary>
    /// Object pool implementation for the lerp operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class QLerpOperationPool<T>
    {
        private static readonly Stack<QLerp_Operation<T>> pool = new();

        public static QLerp_Operation<T> Get(Func<QLerp_Operation<T>> CreateNew)
        {
            return pool.Count > 0 ? pool.Pop() : CreateNew();
        }

        public static void Return(QLerp_Operation<T> operation)
        {
            pool.Push(operation);            
        }
    }
}
