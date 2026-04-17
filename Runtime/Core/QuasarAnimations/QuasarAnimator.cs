using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuasarFramework.Lerp
{
    public class QuasarAnimator : MonoSingleton<QuasarAnimator>
    {
        private readonly List<QLerp> currentOperations = new();

        // TODO (Aaron): Find a better/more memory efficient method to handle adding/removing list elements
        private readonly List<QLerp> toAdd = new();
        private readonly List<QLerp> toRemove = new();

        protected override bool SceneBound => true;
        protected override bool Disposable => true;
        public void AddLerp(QLerp lerp)
        {
            toAdd.Add(lerp);
        }

        public void ReplaceLerp(QLerp initial, QLerp next)
        {
            RemoveLerp(initial);
            next?.Start(initial.toLoop);
        }

        public bool RemoveLerp(QLerp lerp)
        {
            if (currentOperations.Contains(lerp))
            {
                toRemove.Add(lerp);
                return true;
            }
            return false;
        }

        public bool ContainsLerp(QLerp lerp)
        {
            return currentOperations.Contains(lerp);
        }

        private void Update()
        {

            foreach (var remove in toRemove)
            {
                currentOperations.Remove(remove);
            }

            foreach (var add in toAdd)
            {
                currentOperations.Add(add);
            }

            toRemove.Clear();
            toAdd.Clear();

            if (currentOperations.Count == 0) { return; }   
            foreach (var operation in currentOperations)
            {
                operation.UpdateLerp();
            }
        }

    }
    public static class Easing
    {
        public enum EasingTypes
        {
            Linear,
            EaseInSine,
            EaseOutSine,
            EaseInOutSine,
            EaseInQuad,
            EaseOutQuad,
            EaseInOutQuad,
            EaseInCubic,
            EaseOutCubic,
            EaseInOutCubic,
            EaseInQuart,
            EaseOutQuart,
            EaseInOutQuart,
            EaseInQuint,
            EaseOutQuint,
            EaseInOutQuint,
            EaseInExpo,
            EaseOutExpo,
            EaseInOutExpo,
            EaseInCirc,
            EaseOutCirc,
            EaseInOutCirc,
            EaseInBack,
            EaseOutBack,
            EaseInOutBack,
            EaseInElastic,
            EaseOutElastic,
            EaseInOutElastic,
            EaseInBounce,
            EaseOutBounce,
            EaseInOutBounce,

        }


        public static float Linear(float t) => t;

        /// <summary>
        /// https://easings.net/#easeInSine
        /// </summary>
        public static float EaseInSine(float t) => 1 - Mathf.Cos((t * Mathf.PI) * 0.5f);

        /// <summary>
        /// https://easings.net/#easeOutSine
        /// </summary>
        public static float EaseOutSine(float t) => Mathf.Sin((t * Mathf.PI) * 0.5f);

        /// <summary>
        /// https://easings.net/#easeInOutSine
        /// </summary>
        public static float EaseInOutSine(float t) => -(Mathf.Cos(Mathf.PI * t) - 1) * 0.5f;

        /// <summary>
        /// https://easings.net/#easeInQuad
        /// </summary>
        public static float EaseInQuad(float t) => t * t;

        /// <summary>
        /// https://easings.net/#easeOutQuad
        /// </summary>
        public static float EaseOutQuad(float t) => 1 - (1 - t) * (1 - t);

        /// <summary>
        /// https://easings.net/#easeInOutQuad
        /// </summary>
        public static float EaseInOutQuad(float t) => t < 0.5 ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) * 0.5f;

        /// <summary>
        /// https://easings.net/#easeInCubic
        /// </summary>
        public static float EaseInCubic(float t) => t * t * t;

        /// <summary>
        /// https://easings.net/#easeOutCubic
        /// </summary>
        public static float EaseOutCubic(float t) => 1 - Mathf.Pow(1 - t, 3);

        /// <summary>
        /// https://easings.net/#easeInOutCubic
        /// </summary>
        public static float EaseInOutCubic(float t) => t < 0.5 ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) * 0.5f;

        /// <summary>
        /// https://easings.net/#easeInQuart
        /// </summary>
        public static float EaseInQuart(float t) => t * t * t * t;

        /// <summary>
        /// https://easings.net/#easeOutQuart
        /// </summary>
        public static float EaseOutQuart(float t) => 1 - Mathf.Pow(1 - t, 4);

        /// <summary>
        /// https://easings.net/#easeInOutQuart
        /// </summary>
        public static float EaseInOutQuart(float t) => t < 0.5 ? 8 * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 4) * 0.5f;

        /// <summary>
        /// https://easings.net/#easeInQuint
        /// </summary>
        public static float EaseInQuint(float t) => t * t * t * t * t;

        /// <summary>
        /// https://easings.net/#easeOutQuint
        /// </summary>
        public static float EaseOutQuint(float t) => 1 - Mathf.Pow(1 - t, 5);

        /// <summary>
        /// https://easings.net/#easeInOutQuint
        /// </summary>
        public static float EaseInOutQuint(float t) => t < 0.5 ? 16.0f * t * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 5) * 0.5f;

        /// <summary>
        /// https://easings.net/#easeInExpo
        /// </summary>
        public static float EaseInExpo(float t) => Mathf.Approximately(t, 0.0f) ? 0.0f : Mathf.Pow(2, 10 * t - 10);

        /// <summary>
        /// https://easings.net/#easeOutExpo
        /// </summary>
        public static float EaseOutExpo(float t) => Mathf.Approximately(t, 1.0f) ? 1.0f : 1 - Mathf.Pow(2, -10 * t);

        /// <summary>
        /// https://easings.net/#easeInOutExpo
        /// </summary>
        public static float EaseInOutExpo(float t) => Mathf.Approximately(t, 0.0f) ? 0.0f : Mathf.Approximately(t, 1.0f) ? 1.0f : t < 0.5f ? Mathf.Pow(2, 20 * t - 10) * 0.5f : (2 - Mathf.Pow(2, -20 * t + 10)) * 0.5f;

        /// <summary>
        /// https://easings.net/#easeInCirc
        /// </summary>
        public static float EaseInCirc(float t) => 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));

        /// <summary>
        /// https://easings.net/#easeOutCirc
        /// </summary>
        public static float EaseOutCirc(float t) => Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));

        /// <summary>
        /// https://easings.net/#easeInOutCirc
        /// </summary>
        public static float EaseInOutCirc(float t) => t < 0.5 ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t, 2))) * 0.5f : (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) * 0.5f;

        /// <summary>
        /// https://easings.net/#easeInBack
        /// </summary>
        public static float EaseInBack(float t) => (1.70158f + 1) * t * t * t - 1.70158f * t * t;

        /// <summary>
        /// https://easings.net/#easeOutBack
        /// </summary>
        public static float EaseOutBack(float t) => 1 + (1.70158f + 1) * Mathf.Pow(t - 1, 3) + 1.70158f * Mathf.Pow(t - 1, 2);

        /// <summary>
        /// https://easings.net/#easeInOutBack
        /// </summary>
        public static float EaseInOutBack(float t)
        {
            const float c2 = 1.70158f * 1.525f;
            return t < 0.5 ? (Mathf.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2)) * 0.5f : (Mathf.Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;
        }

        /// <summary>
        /// https://easings.net/#easeInElastic
        /// </summary>
        public static float EaseInElastic(float t)
        {
            const float c4 = (2 * Mathf.PI) / 3;
            return Mathf.Approximately(t, 0.0f) ? 0.0f : Mathf.Approximately(t, 1.0f) ? 1.0f : -Mathf.Pow(2f, 10f * t - 10) * Mathf.Sin((t * 10 - 10.75f) * c4);
        }

        /// <summary>
        /// https://easings.net/#easeOutBack
        /// </summary>
        public static float EaseOutElastic(float t)
        {
            const float c4 = (2 * Mathf.PI) / 3;
            return Mathf.Approximately(t, 0.0f) ? 0.0f : Mathf.Approximately(t, 1.0f) ? 1.0f : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * c4) + 1;
        }

        /// <summary>
        /// https://easings.net/#easeInOutElastic
        /// </summary>
        public static float EaseInOutElastic(float t) => t < 0.5 ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t, 2))) * 0.5f : (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) * 0.5f;

        public static float EaseInBounce(float t) { return 1 - EaseOutBounce(1 - t); }

        public static float EaseOutBounce(float t)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (t < 1 / d1)
            {
                return n1 * t * t;
            }
            else if (t < 2 / d1)
            {
                return n1 * (t -= 1.5f / d1) * t + 0.75f;
            }
            else if (t < 2.5f / d1)
            {
                return n1 * (t -= 2.25f / d1) * t + 0.9375f;
            }
            else
            {
                return n1 * (t -= 2.625f / d1) * t + 0.984375f;
            }
        }

        public static float EaseInOutBounce(float t)
        {
            return t < 0.5f ? (1 - EaseOutBounce(1 - 2 * t)) * 0.5f : (1 + EaseOutBounce(2 * t - 1)) * 0.5f;
        }


        private static readonly Dictionary<EasingTypes, Func<float, float>> _map =
            new Dictionary<EasingTypes, Func<float, float>>
            {
                { EasingTypes.Linear, Linear },

                { EasingTypes.EaseInSine, EaseInSine },
                { EasingTypes.EaseOutSine, EaseOutSine },
                { EasingTypes.EaseInOutSine, EaseInOutSine },

                { EasingTypes.EaseInQuad, EaseInQuad },
                { EasingTypes.EaseOutQuad, EaseOutQuad },
                { EasingTypes.EaseInOutQuad, EaseInOutQuad },

                { EasingTypes.EaseInCubic, EaseInCubic },
                { EasingTypes.EaseOutCubic, EaseOutCubic },
                { EasingTypes.EaseInOutCubic, EaseInOutCubic },

                { EasingTypes.EaseInQuart, EaseInQuart },
                { EasingTypes.EaseOutQuart, EaseOutQuart },
                { EasingTypes.EaseInOutQuart, EaseInOutQuart },

                { EasingTypes.EaseInQuint, EaseInQuint },
                { EasingTypes.EaseOutQuint, EaseOutQuint },
                { EasingTypes.EaseInOutQuint, EaseInOutQuint },

                { EasingTypes.EaseInExpo, EaseInExpo },
                { EasingTypes.EaseOutExpo, EaseOutExpo },
                { EasingTypes.EaseInOutExpo, EaseInOutExpo },

                { EasingTypes.EaseInCirc, EaseInCirc },
                { EasingTypes.EaseOutCirc, EaseOutCirc },
                { EasingTypes.EaseInOutCirc, EaseInOutCirc },

                { EasingTypes.EaseInBack, EaseInBack },
                { EasingTypes.EaseOutBack, EaseOutBack },
                { EasingTypes.EaseInOutBack, EaseInOutBack },

                { EasingTypes.EaseInElastic, EaseInElastic },
                { EasingTypes.EaseOutElastic, EaseOutElastic },
                { EasingTypes.EaseInOutElastic, EaseInOutElastic },

                { EasingTypes.EaseInBounce, EaseInBounce },
                { EasingTypes.EaseOutBounce, EaseOutBounce },
                { EasingTypes.EaseInOutBounce, EaseInOutBounce },
            };

        public static Func<float, float> GetEasingFunc(EasingTypes type)
        {
            return _map[type];
        }
    }
}
