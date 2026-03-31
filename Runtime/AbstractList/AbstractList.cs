using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuasarFramework.AbstractList
{
    [Serializable]
    public class AbstractList<T>
    {
        [SerializeReference]
        public List<T> items = new List<T>();
    }
}
