using QuasarFramework.Lerp;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace EhHowAh.VirtualSandbox.UI
{
    [System.Serializable]
    public class GraphicColorFade : UIAnimationMono<Graphic>
    {
        [SerializeField] Color targetColor;
        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            Cache(controller);
            Color currentColor = target.color;
            return lerp.Color(currentColor, targetColor, duration, c => target.color = c, Easing.GetEasingFunc(easing));
        }
    }
}
