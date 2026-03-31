using QuasarFramework.Lerp;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    [System.Serializable]
    public class CreateNewSequence : UIAnimation
    {
        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            return lerp.Sequence(duration);
        }
    }
}
