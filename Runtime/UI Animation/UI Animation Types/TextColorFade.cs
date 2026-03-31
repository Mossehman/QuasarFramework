using QuasarFramework.Lerp;
using System;
using TMPro;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    [System.Serializable]
    public class TextColorFade : UIAnimationMono<TMP_Text>
    {
        [SerializeField] Color targetColor;
        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            Cache(controller);
            Color currentColor = target.color;
            Func<float, float> easingFunc = Easing.GetEasingFunc(easing);
            return lerp.Color(currentColor, targetColor, duration, c => target.color = c, easingFunc);
        }
    }
}