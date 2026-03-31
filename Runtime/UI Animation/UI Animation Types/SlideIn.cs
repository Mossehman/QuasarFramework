using QuasarFramework.Lerp;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    public class SlideIn : UIAnimation
    {
        [SerializeField] RectTransform target;
        [SerializeField] ScreenEdge edge;
        RectTransform canvasRect;
        Vector3? originalPosition = null;

        public enum ScreenEdge
        {
            Left,
            Right,
            Top,
            Bottom
        }

        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            if (!canvasRect)
            {
                canvasRect = target.GetComponentInParent<Canvas>().GetComponent<RectTransform>(); // Get the canvas rect
            }


            if (originalPosition == null)
            {
                originalPosition = target.localPosition;
            }
            Vector3 startPosition = AlignCanvasEdge(target, canvasRect, edge);
            target.localPosition = startPosition;
            return lerp.Vector(startPosition, originalPosition.Value, duration, v => target.localPosition = v, Easing.GetEasingFunc(easing)).OnComplete(() =>
            {
                target.localPosition = originalPosition.Value;
                originalPosition = null;
            });
        }

        private Vector3 AlignCanvasEdge(RectTransform target, RectTransform canvas, ScreenEdge edge)
        {
            Vector3[] targetCorners = new Vector3[4];
            Vector3[] canvasCorners = new Vector3[4];

            target.GetLocalCorners(targetCorners);
            canvas.GetLocalCorners(canvasCorners);

            // Corner order:
            // 0 = bottom left
            // 1 = top left
            // 2 = top right
            // 3 = bottom right

            Vector3 offset = Vector3.zero;

            switch (edge)
            {
                case ScreenEdge.Left:
                    offset.x = canvasCorners[2].x - targetCorners[0].x;
                    break;

                case ScreenEdge.Right:
                    offset.x = canvasCorners[0].x - targetCorners[2].x;
                    break;

                case ScreenEdge.Top:
                    offset.y = canvasCorners[0].y - targetCorners[1].y;
                    break;

                case ScreenEdge.Bottom:
                    offset.y = canvasCorners[1].y - targetCorners[0].y;
                    break;
            }

            return target.localPosition + offset;
        }
    }
}
