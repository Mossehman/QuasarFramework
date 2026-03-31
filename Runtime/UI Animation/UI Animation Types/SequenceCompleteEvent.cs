using QuasarFramework.Lerp;
using UnityEngine;
using UnityEngine.Events;

namespace EhHowAh.VirtualSandbox.UI
{
    public class SequenceCompleteEvent : UIAnimation
    {
        [SerializeField] UnityEvent<UIAnimationController> onComplete;

        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            return lerp.OnComplete(() => onComplete?.Invoke(controller));
        }
    }
}
