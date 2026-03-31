using QuasarFramework.Lerp;
using QuasarFramework.AbstractList;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    /// <summary>
    /// Script that will allow us to control the animations of our UI Elements programatically via Inspector
    /// <br>
    /// Implemented specifically 
    /// </summary>
    public class UIAnimationController : MonoBehaviour
    {
        [SerializeField] bool toLoop = false;
        [SerializeField] AbstractList<UIAnimation> onOpen;
        [SerializeField] AbstractList<UIAnimation> onClose;
        QLerp uiLerp;

        /// <summary>
        /// Trigger to play the animations when the UI is shown
        /// </summary>
        public void OpenUI()
        {
            uiLerp?.Stop(); // Stop the existing lerp operation
            uiLerp = new QLerp(); // Create new QLerp instance (Garbage Collector be damned)

            foreach (var animation in onOpen.items)
            {
                uiLerp = animation.OnPlayAnimation(this, uiLerp); // Add all existing lerp operations to the sequence
            }
            uiLerp.GetRoot().Start(toLoop);
        }

        /// <summary>
        /// Trigger to play the animations when the UI is hidden
        /// </summary>
        public void CloseUI()
        {
            uiLerp?.Stop(); // Stop the existing lerp operation
            uiLerp = new QLerp(); // Create new QLerp instance (Garbage Collector be damned)

            foreach (var animation in onClose.items)
            {
                uiLerp = animation.OnPlayAnimation(this, uiLerp); // Add all existing lerp operations to the sequence
            }
            uiLerp.GetRoot().Start(toLoop);
        }
    }
}
