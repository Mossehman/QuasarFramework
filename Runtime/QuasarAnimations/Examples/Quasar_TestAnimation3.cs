using QuasarFramework.Lerp;
using UnityEngine;

public class Quasar_TestAnimation3 : MonoBehaviour
{
    [SerializeField] Vector3 initialPosition;
    [SerializeField] Vector3 offset;

    [SerializeField] float time = 0.2f;

    private void Start()
    {
        initialPosition = transform.position;
        Vector3 offsetPosition = initialPosition + offset;
        Vector3 initialScale = transform.localScale;

        QLerp lerp = new QLerp().Vector(initialPosition, offsetPosition, time, v => transform.position = v, Easing.EaseInQuad)
                                .Vector(initialScale * 0.5f, initialScale * 1.1f, time * 1.2f, v => transform.localScale = v, Easing.EaseInQuad, 0.1f)
                                .Start()
                                .Sequence()
                                .Vector(initialScale * 1.1f, initialScale, time * 0.1f, v => transform.localScale = v, Easing.EaseInQuad)
                                .Sequence(0.5f)
                                .Vector(initialScale, initialScale * 0.85f, time, v => transform.localScale = v, Easing.EaseInQuad)
                                .OnComplete(Jump)
                                .Sequence(0.1f)
                                .Vector(initialScale * 0.5f, initialScale, time * 0.3f, v => transform.localScale = v, Easing.EaseInQuad)
                                .Vector(Vector3.zero, new Vector3(0, 180, 0), time * 0.3f, v => transform.eulerAngles = v, Easing.EaseInQuad);
    }
    
    private void Jump()
    {
        Vector3 jumpVector = offset + new Vector3(0, 5.0f, 0);
        QLerp lerp = new QLerp().Vector(initialPosition, jumpVector, time * 0.5f, v => transform.position = v, Easing.EaseInQuad)
                                .Start()
                                .Sequence()
                                .Vector(jumpVector, initialPosition, time * 0.5f, v => transform.position = v, Easing.EaseInQuad);
    }
}
