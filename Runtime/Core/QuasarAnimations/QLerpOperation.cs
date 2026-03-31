using System;
using UnityEngine;

namespace QuasarFramework.Lerp
{
    public abstract class QLerp_Operation<T> : I_QLerpOperation
    {
        protected T startValue;
        protected T endValue;
        protected Action<T> onValue;

        protected float duration;

        protected Func<float, float> easing;
        protected float power;

        public event Action onComplete;
        public float Duration => duration;

        public bool isComplete = false;

        public QLerp_Operation<T> Initialise(T startValue, T endValue, float duration, Action<T> onValue, Func<float, float> easing, float power = 1)
        {
            this.startValue = startValue;
            this.endValue = endValue;

            this.duration = duration;

            this.onValue = onValue;
            this.easing = easing;
            this.power = power;

            isComplete = false;

            return this;
        }

        public void Lerp(float elapsedTime)
        {
            if (isComplete) { return; }
            float t = elapsedTime / duration;
            t = Mathf.Clamp01(t);

            float remappedT = easing != null ? Remap(easing(t), power) : t;
            HandleLerp(remappedT);

            if (t >= 1.0f)
            {
                remappedT = 1.0f;
                onComplete?.Invoke();
                isComplete = true;
                return;
            }

        }

        /// <summary>
        /// Handles the lerp logic (different types require different lerp logic)
        /// </summary>
        /// <param name="t">Variable t, represents lerp progression</param>
        protected abstract void HandleLerp(float t);

        public void Return()
        {
            onValue = null;
            onComplete = null;
            easing = null;
            duration = 0f;
            isComplete = false;
            QLerpOperationPool<T>.Return(this);
        }
        
        public void Reset()
        {
            isComplete = false;
        }

        private float Remap(float t, float pow)
        {
            pow = Mathf.Max(0.001f, pow);
            return Mathf.Pow(t, pow);
        }
    }

    public class QLerp_FloatOperation : QLerp_Operation<float>
    {
        protected override void HandleLerp(float t)
        {
            onValue?.Invoke(Mathf.LerpUnclamped(startValue, endValue, t));
        }
    }

    public class QLerp_VectorOperation : QLerp_Operation<Vector3>
    {
        protected override void HandleLerp(float t)
        {
            onValue.Invoke(Vector3.LerpUnclamped(startValue, endValue, t));
        }
    }

    public class QLerp_ColorOperation : QLerp_Operation<Color>
    {
        protected override void HandleLerp(float t)
        {
            onValue.Invoke(Color.LerpUnclamped(startValue, endValue, t));
        }
    }

    public class QLerp_QuaternionOperation : QLerp_Operation<Quaternion>
    {
        protected override void HandleLerp(float t)
        {
            onValue.Invoke(Quaternion.LerpUnclamped(startValue, endValue, t));
        }
    }
}
