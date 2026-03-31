using UnityEngine;

/// <summary>
/// Data for inspector documentation to display
/// <br>Uses a scriptable object to allow for global changes/edits
/// </summary>
[CreateAssetMenu(fileName = "New Documentation", menuName = "Documentation")]
public sealed class DocumentationSO : ScriptableObject
{
    [SerializeField] DocumentationData data;

    [System.Serializable]
    public class DocumentationData
    {
        [SerializeField] public string title;
        [TextArea(1, 200), SerializeField] public string documentation;
        public bool isEditing = true;
    }
}
