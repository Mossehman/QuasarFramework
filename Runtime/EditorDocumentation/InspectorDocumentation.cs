using UnityEngine;

/// <summary>
/// Display class for our documentation data object in editor GUI
/// </summary>
[System.Serializable]
public sealed class InspectorDocumentation
{
    public DocumentationSO documentation;
    public bool isEditing = true;

    public bool IsEditing() { return isEditing; }
}
