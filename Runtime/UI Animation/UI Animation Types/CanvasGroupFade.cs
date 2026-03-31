using QuasarFramework.Lerp;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    public class CanvasGroupFade : UIAnimation
    {
        [SerializeField] CanvasGroup target;
        [SerializeField, Range(0f, 1f)] float targetAlpha;

        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            float currAlpha = target.alpha;
            lerp.Float(currAlpha, targetAlpha, duration, f => target.alpha = f, Easing.GetEasingFunc(easing));
            return lerp;
        }
    }
}
