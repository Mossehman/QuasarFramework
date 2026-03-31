using QuasarFramework.Lerp;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    public class RectScaleToFrom : UIAnimation
    {
        [SerializeField] Transform target;
        [SerializeField] Vector3 startScale;
        [SerializeField] Vector3 targetScale;
        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            return lerp.Vector(startScale, targetScale, duration, v => target.localScale = v, Easing.GetEasingFunc(easing));
        }
    }
}
