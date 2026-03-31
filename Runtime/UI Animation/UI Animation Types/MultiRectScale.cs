using QuasarFramework.Lerp;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    public class MultiRectScale : UIAnimation
    {
        [SerializeField] RectTransform[] targets;
        [SerializeField] Vector3 targetScale;
        [SerializeField] float interval = 0.1f;

        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            float interval = 0.0f;
            foreach (var t in targets)
            {
                Vector3 currScale = t.localScale;
                new QLerp().Sequence(interval).Vector(currScale, targetScale, duration, v => t.localScale = v, Easing.GetEasingFunc(easing)).Start();
                interval += this.interval;
            }

            lerp.Float(0.0f, 1.0f, interval + duration); // Add this here so we can still signal when the lerps are completed
            return lerp;
        }
    }
}
