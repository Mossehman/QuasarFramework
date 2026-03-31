using System.Collections.Generic;
using System;
using UnityEngine;

namespace QuasarFramework.Lerp
{
    public static class QLerpPool
    {
        private static readonly Stack<QLerp> pool = new();

        public static QLerp Get(Func<QLerp> CreateNew)
        {
            return pool.Count > 0 ? pool.Pop() : CreateNew();
        }

        public static void Return(QLerp operation)
        {
            pool.Push(operation);
        }
    }
}
