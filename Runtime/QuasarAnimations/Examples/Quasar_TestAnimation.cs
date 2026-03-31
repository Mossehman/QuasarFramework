using QuasarFramework.Lerp;
using UnityEngine;

public class Quasar_TestAnimation : MonoBehaviour
{
    [SerializeField] Vector3 initialPosition;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float time;

    QLerp lerp1;
    QLerp lerp2;

    void Start()
    {
        initialPosition = transform.position;
        lerp1 = new QLerp().Vector(initialPosition, targetPosition, time, v => transform.position = v, Easing.EaseInQuad, 0.5f)
            .Vector(Vector3.one, Vector3.one * 2, time, v => transform.localScale = v, Easing.EaseInQuad, 0.5f).Start(true)
            .Sequence(1.5f)
            .Vector(targetPosition, new Vector3(targetPosition.x, targetPosition.y + 2.0f, targetPosition.z), time, v => transform.position = v, Easing.EaseInQuad, 0.5f)
            .Vector(Vector3.one * 2, Vector3.one, time, v => transform.localScale = v, Easing.EaseInQuad, 0.5f)
            .Sequence(2.0f)
            .Vector(new Vector3(targetPosition.x, targetPosition.y + 2.0f, targetPosition.z), new Vector3(targetPosition.x, targetPosition.y - 2.0f, targetPosition.z), time, v => transform.position = v, Easing.EaseInQuad, 0.5f)
            .Vector(Vector3.one, Vector3.zero, time, v => transform.localScale = v, Easing.EaseInQuad, 0.5f)
            .Sequence(0.5f)
            .Vector(Vector3.zero, Vector3.one * 2, time, v => transform.localScale = v, Easing.EaseInQuad, 0.5f)
            .Sequence(1.0f)
            .Vector(Vector3.one * 2, Vector3.one, time * 2, v => transform.localScale = v, Easing.EaseInQuad, 0.5f)
            .Vector(new Vector3(targetPosition.x, targetPosition.y - 2.0f, targetPosition.z), initialPosition, time * 2, v => transform.position = v, Easing.EaseInQuad, 0.5f);

        lerp2 = new QLerp().Vector(Vector3.zero, new Vector3(0, 360, 0), 5.0f, v => transform.eulerAngles = v, Easing.Linear, 1.0f).Start(true);

    }

    [ContextMenu("Pause")]
    public void StopLerps()
    {
        lerp1.Pause();
        lerp2.Pause();
    }

    [ContextMenu("Resume")]
    public void ResumeLerps()
    {
        lerp1.Resume();
        lerp2.Resume();
    }
}
