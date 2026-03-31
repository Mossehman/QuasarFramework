using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Abstracted editor property class for handling the documentation GUI
/// <br>Since the documentation should display the same for the raw data (Scriptable Object) and the class (InspectorDocumentation), we use OOP
/// </summary>
public abstract class DocumentationGUI : PropertyDrawer
{
    protected GUIStyle richTextStyle;
    protected GUIStyle titleStyle;
    protected GUIStyle buttonStyle;

    private void EnsureStyles()
    {
        if (richTextStyle == null)
        {
            richTextStyle = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                wordWrap = true
            };
        }

        if (titleStyle == null)
        {
            titleStyle = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                wordWrap = true,
                fontSize = 24,
                fontStyle = FontStyle.Bold
            };
        }

        if (buttonStyle == null)
        {
            buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 14,
                normal = { textColor = Color.white }
            };
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EnsureStyles();
        EditorGUI.BeginProperty(position, label, property);

        property.isExpanded = EditorGUI.Foldout(
            new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
            property.isExpanded,
            label,
            true
            );

        if (property.isExpanded)
        {
            SerializedProperty titleProp = GetTitleProp(property);
            SerializedProperty documentationProp = GetDocumentationProp(property);
            SerializedProperty editingProp = GetEditingProp(property);

            GUILayout.BeginVertical(EditorStyles.helpBox);
            if (editingProp.boolValue)
            {
                DrawEditing(property); // Handle the editing GUI separately in derived classes
            }
            else
            {
                // Since our final documentation should look the same throughout, we can draw it directly in our base class
                if (titleProp != null)
                {

                    EditorGUILayout.LabelField(titleProp.stringValue, titleStyle);
                    EditorGUILayout.Space(10);
                }

                if (documentationProp != null)
                {
                    DrawDocumentation(documentationProp.stringValue);
                }

                EditorGUILayout.Space();

                if (GUILayout.Button("Edit", buttonStyle))
                {
                    editingProp.boolValue = true;
                    property.serializedObject.ApplyModifiedProperties();
                }

            }
            GUILayout.EndVertical();

        }
        EditorGUI.EndProperty();
    }

    private void DrawDocumentation(string text)
    {
        Regex imgTagRegex = new Regex(
            @"<img\s+src=(?<src>[^\s>]+)(?:\s+width=(?<width>[^\s>]+))?(?:\s+height=(?<height>[^\s>]+))?\s*>",
            RegexOptions.IgnoreCase);

        int currentIndex = 0;

        foreach (Match match in imgTagRegex.Matches(text))
        {
            if (match.Index > currentIndex)
            {
                string beforeText = text.Substring(currentIndex, match.Index - currentIndex);
                EditorGUILayout.LabelField(beforeText, richTextStyle);
            }

            string imagePath = match.Groups["src"].Value.Trim('"');
            string widthStr = match.Groups["width"].Value;
            string heightStr = match.Groups["height"].Value;
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            if (texture != null)
            {
                float textureAspect = (float)texture.width / texture.height;
                float width, height;

                bool hasWidth = float.TryParse(widthStr, out width);
                bool hasHeight = float.TryParse(heightStr, out height);

                if (hasWidth && hasHeight)
                {
                }
                else if (hasWidth)
                {
                    height = width / textureAspect;
                }
                else if (hasHeight)
                {
                    width = height * textureAspect;
                }
                else
                {
                    height = 64f;
                    width = height * textureAspect;
                }
                GUILayout.Label(texture, GUILayout.Width(width), GUILayout.Height(height));
            }
            else
            {
                EditorGUILayout.HelpBox($"Image not found: {imagePath}", MessageType.Warning);
            }
            currentIndex = match.Index + match.Length;
        }
        if (currentIndex < text.Length)
        {
            string afterText = text.Substring(currentIndex);
            EditorGUILayout.LabelField(afterText, richTextStyle);
        }
    }

    // 

    protected abstract SerializedProperty GetTitleProp(SerializedProperty property);
    protected abstract SerializedProperty GetDocumentationProp(SerializedProperty property);
    protected abstract SerializedProperty GetEditingProp(SerializedProperty property);

    
    protected abstract void DrawEditing(SerializedProperty property);
}
