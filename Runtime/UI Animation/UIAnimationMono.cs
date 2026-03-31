using QuasarFramework.Lerp;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{

    /// <summary>
    /// Base class for our UI Animations, allows us to use Injection to define animations programatically
    /// </summary>
    [System.Serializable]
    public abstract class UIAnimationMono<T> : UIAnimation where T : MonoBehaviour
    {
        [SerializeField] protected T target;

        /// <summary>
        /// Caches the target variable on startup to prevent any load time issues
        /// </summary>
        /// <param name="controller">The animation controller</param>
        public void Cache(UIAnimationController controller) { if (target == null) { target = controller.GetComponent<T>(); } }
    }
}
