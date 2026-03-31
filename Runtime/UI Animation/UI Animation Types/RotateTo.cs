using QuasarFramework.Lerp;
using UnityEngine;

namespace EhHowAh.VirtualSandbox.UI
{
    public class RotateTo : UIAnimation
    {
        public enum RotationDirection
        {
            Clockwise,
            CounterClockwise,
            Closest
        }

        [SerializeField] RectTransform target;
        [SerializeField, Range(0f, 360f)] float targetRotation;
        [SerializeField] RotationDirection rotationDirection = RotationDirection.Closest;
        float? originalZ;

        float GetTargetAngle(float current, float target)
        {
            float delta = Mathf.DeltaAngle(current, target);

            // Special case: when the shortest path is zero but target is 360°
            if (delta == 0f)
            {
                // Only apply full rotation if target is exactly 360 and current is near 0
                if (Mathf.Approximately(target, 360f) && Mathf.Approximately(current, 0f))
                {
                    switch (rotationDirection)
                    {
                        case RotationDirection.Clockwise:
                            return current - 360f;   // full clockwise rotation
                        case RotationDirection.CounterClockwise:
                            return current + 360f;   // full counterclockwise rotation
                        default:
                            return current;
                    }
                }
                return current;
            }

            // Normal direction handling
            switch (rotationDirection)
            {
                case RotationDirection.Clockwise:
                    if (delta > 0) delta -= 360f;
                    break;
                case RotationDirection.CounterClockwise:
                    if (delta < 0) delta += 360f;
                    break;
            }
            return current + delta;
        }

        public override QLerp OnPlayAnimation(UIAnimationController controller, QLerp lerp)
        {
            originalZ ??= target.localEulerAngles.z;

            float startZ = originalZ.Value;
            float endZ = GetTargetAngle(startZ, targetRotation);

            lerp.Float(
                startZ,
                endZ,
                duration,
                z =>
                {
                    Vector3 e = target.localEulerAngles;
                    e.z = z;
                    target.localEulerAngles = e;
                },
                Easing.GetEasingFunc(easing)
            );

            return lerp;
        }
    }
}
