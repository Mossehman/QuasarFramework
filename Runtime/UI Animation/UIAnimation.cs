using QuasarFramework.Lerp;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    [System.Serializable]
    public abstract class UIAnimation
    {
        [SerializeField, Tooltip("The duration of the lerp animation")] protected float duration = 1.0f;
        [SerializeField, Tooltip("The easing curve for the lerp animation")] protected Easing.EasingTypes easing;

        /// <summary>
        /// Abstracted function for when the animation starts playing, different animation types can be defined in derived classes (Fade, Shrink, etc)
        /// </summary>
        /// <param name="controller"></param>
        public abstract QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp);
    }
}
