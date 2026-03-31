using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DocumentationSO.DocumentationData))]
public class DocDataGUI : DocumentationGUI
{
    protected override void DrawEditing(SerializedProperty property)
    {
        EditorGUILayout.PropertyField(GetTitleProp(property), new GUIContent("Title"));
        EditorGUILayout.PropertyField(GetDocumentationProp(property), new GUIContent("Documentation"));

        if (GUILayout.Button("Save", buttonStyle))
        {
            GetEditingProp(property).boolValue = false;
            property.serializedObject.ApplyModifiedProperties();
        }
    }

    protected override SerializedProperty GetDocumentationProp(SerializedProperty property)
    {
        return property.FindPropertyRelative("documentation");
    }

    protected override SerializedProperty GetEditingProp(SerializedProperty property)
    {
        return property.FindPropertyRelative("isEditing");
    }

    protected override SerializedProperty GetTitleProp(SerializedProperty property)
    {
        return property.FindPropertyRelative("title");
    }
}
