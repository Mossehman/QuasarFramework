using QuasarFramework.Lerp;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    public class MoveToRect : UIAnimation
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private RectTransform to;
        [SerializeField] private RectTransform dest;

        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            Vector3 originalPos = to ? to.position : target.position;
            lerp.Vector(originalPos, dest.position, duration, v => target.position = v, Easing.GetEasingFunc(easing));
            return lerp;

        }
    }
}
