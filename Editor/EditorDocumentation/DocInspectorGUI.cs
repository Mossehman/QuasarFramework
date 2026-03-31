using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InspectorDocumentation))]
public class DocInspectorGUI : DocumentationGUI
{
    protected override void DrawEditing(SerializedProperty property)
    {
        SerializedProperty SOProp = property.FindPropertyRelative("documentation");
        EditorGUILayout.PropertyField(SOProp, new GUIContent("Data"));
        if (GUILayout.Button("Save", buttonStyle))
        {
            GetEditingProp(property).boolValue = false;
            property.serializedObject.ApplyModifiedProperties();
        }
    }

    protected override SerializedProperty GetDocumentationProp(SerializedProperty property)
    {
        SerializedProperty documentationProp = property.FindPropertyRelative("documentation");
        if (documentationProp.objectReferenceValue == null)
            return null;

        SerializedObject so = new SerializedObject(documentationProp.objectReferenceValue);
        SerializedProperty dataProp = so.FindProperty("data");

        return dataProp.FindPropertyRelative("documentation");
    }

    protected override SerializedProperty GetEditingProp(SerializedProperty property)
    {
        return property.FindPropertyRelative("isEditing");
    }

    protected override SerializedProperty GetTitleProp(SerializedProperty property)
    {
        SerializedProperty documentationProp = property.FindPropertyRelative("documentation");
        if (documentationProp.objectReferenceValue == null)
            return null;

        SerializedObject so = new SerializedObject(documentationProp.objectReferenceValue);
        SerializedProperty dataProp = so.FindProperty("data");

        return dataProp.FindPropertyRelative("title");
    }
}
