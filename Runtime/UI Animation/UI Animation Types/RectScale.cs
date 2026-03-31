using QuasarFramework.Lerp;
using System;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    [System.Serializable]
    public class RectScale : UIAnimation
    {
        [SerializeField] Transform target;
        [SerializeField] Vector3 targetScale;
        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            Vector3 currentScale = target.localScale;
            Func<float, float> easingFunc = Easing.GetEasingFunc(easing);
            return lerp.Vector(currentScale, targetScale, duration, v => target.localScale = v, easingFunc);
        }
    }
}
