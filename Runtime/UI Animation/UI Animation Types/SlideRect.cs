using QuasarFramework.Lerp;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    public class SlideRect : UIAnimation
    {
        [SerializeField] RectTransform target;
        [SerializeField] RectEdge edge;
        [SerializeField] RectTransform slideBounds;


        public enum RectEdge
        {
            Left,
            Right,
            Top,
            Bottom
        }

        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            if (!slideBounds)
            {
                slideBounds = target.parent.GetComponent<RectTransform>(); // Get the canvas rect
            }

            Vector3 startPosition = target.localPosition;
            Vector3 targetPosition = AlignCanvasEdge(target, slideBounds, edge);
            return lerp.Vector(startPosition, targetPosition, duration, v => target.localPosition = v, Easing.GetEasingFunc(easing));
        }

        private Vector3 AlignCanvasEdge(RectTransform target, RectTransform canvas, RectEdge edge)
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
                case RectEdge.Left:
                    offset.x = canvasCorners[2].x - targetCorners[2].x;
                    break;

                case RectEdge.Right:
                    offset.x = canvasCorners[0].x - targetCorners[0].x;
                    break;

                case RectEdge.Top:
                    offset.y = canvasCorners[0].y - targetCorners[0].y;
                    break;

                case RectEdge.Bottom:
                    offset.y = canvasCorners[1].y - targetCorners[1].y;
                    break;
            }

            return target.localPosition + offset * 2;
        }
    }
}
