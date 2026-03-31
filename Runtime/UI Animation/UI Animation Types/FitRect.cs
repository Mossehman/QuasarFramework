using QuasarFramework.Lerp;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    public class FitRect : UIAnimation
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private RectTransform startRect;
        [SerializeField] private RectTransform endRect;

        Vector3 currentBL;
        Vector3 currentTR;

        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            RectTransform start = target ? target : startRect;

            Vector3[] startCorners = new Vector3[4];
            Vector3[] endCorners = new Vector3[4];

            start.GetWorldCorners(startCorners);
            endRect.GetWorldCorners(endCorners);

            Vector3 startBL = startCorners[0];
            Vector3 startTR = startCorners[2];

            Vector3 endBL = endCorners[0];
            Vector3 endTR = endCorners[2];

            currentBL = startBL;
            currentTR = startTR;

            var ease = Easing.GetEasingFunc(easing);

            lerp.Vector(startBL, endBL, duration, v =>
                {
                    currentBL = v;
                    Apply();
                }, 
                ease)
                .Vector(startTR, endTR, duration, v =>
                {
                    currentTR = v;
                    Apply();
                }, 
                ease);

            return lerp;
        }

        void Apply()
        {
            Vector3 worldCenter = (currentBL + currentTR) * 0.5f;
            Vector2 size = currentTR - currentBL;

            RectTransform parent = target.parent as RectTransform;
            Vector2 localCenter = parent.InverseTransformPoint(worldCenter);

            target.anchoredPosition = localCenter;
            target.sizeDelta = size;
        }
    }

}
