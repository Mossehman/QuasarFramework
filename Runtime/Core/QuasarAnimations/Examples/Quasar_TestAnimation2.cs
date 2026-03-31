using QuasarFramework.Lerp;
using UnityEngine;
using UnityEngine.UI;

public class Quasar_TestAnimation2 : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Color color;
    [SerializeField] Color color2;

    private void Start()
    {
        Color initial = image.color;
        QLerp newLerp = new QLerp().Color(initial, color, 2.0f, v => image.color = v, Easing.Linear, 1.0f).Start(true)
            .Sequence()
            .Color(color, color2, 2.0f, v => image.color = v, Easing.Linear, 1.0f)
            .Sequence()
            .Color(color2, initial, 1.0f, v => image.color = v, Easing.EaseInQuad, 1.0f);
    }
}
