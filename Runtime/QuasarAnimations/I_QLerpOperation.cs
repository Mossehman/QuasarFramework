using System;
using UnityEngine;

namespace QuasarFramework.Lerp
{
    public interface I_QLerpOperation
    {
        void Lerp(float elapsedTime);
        float Duration { get; }
        event Action onComplete;

        void Return();
        void Reset();
    }
}
