using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System.Collections.Generic;

namespace QuasarFramework.AbstractList
{


    [CustomPropertyDrawer(typeof(AbstractList<>), true)]
    public class AbstractListDrawer : PropertyDrawer
    {
        private Dictionary<string, ReorderableList> lists = new Dictionary<string, ReorderableList>();


        private Type[] derivedTypes;
        private string[] derivedNames;
        private Type elementType;

        private void Init(SerializedProperty property)
        {
            if (derivedTypes != null)
                return;

            Type listType = fieldInfo.FieldType;
            elementType = listType.GetGenericArguments()[0];


            derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t =>
                    !t.IsAbstract &&
                    elementType.IsAssignableFrom(t))
                .ToArray();

            derivedNames = derivedTypes.Select(t => t.Name).ToArray();
        }


        private ReorderableList GetList(SerializedProperty property)
        {
            if (lists.TryGetValue(property.propertyPath, out var existingList))
                return existingList;

            SerializedProperty itemsProp = property.FindPropertyRelative("items");

            var newList = new ReorderableList(
                property.serializedObject,
                itemsProp,
                true, true, true, true
            );

            ConfigureList(newList, property);

            lists[property.propertyPath] = newList;

            return newList;
        }

        private void ConfigureList(ReorderableList list, SerializedProperty rootProperty)
        {
            // HEADER
            list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, rootProperty.displayName);
            };

            // ELEMENT HEIGHT
            list.elementHeightCallback = (int index) =>
            {
                SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(element, includeChildren: true) + 6;
            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;

                // Determine type name (from managedReferenceFullTypename)
                string fullType = element.managedReferenceFullTypename;
                string typeName = string.IsNullOrEmpty(fullType)
                    ? "(Null)"
                    : fullType.Split(' ').Last().Split('.').Last(); // extract class name

                float height = EditorGUI.GetPropertyHeight(element, includeChildren: true);
                rect.height = height;

                EditorGUI.PropertyField(rect, element, new GUIContent(typeName), includeChildren: true);
            };

            list.onAddCallback = (ReorderableList rl) =>
            {
                ShowAddMenu(rl.serializedProperty, rootProperty);
            };
        }

        private void ShowAddMenu(SerializedProperty arrayProp, SerializedProperty rootProperty)
        {
            GenericMenu menu = new GenericMenu();

            for (int i = 0; i < derivedTypes.Length; i++)
            {
                int idx = i;

                menu.AddItem(new GUIContent(derivedNames[i]), false, () =>
                {
                    arrayProp.arraySize++;
                    var element = arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1);

                    element.managedReferenceValue =
                        Activator.CreateInstance(derivedTypes[idx]);

                    rootProperty.serializedObject.ApplyModifiedProperties();
                });
            }

            menu.ShowAsContext();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //float height = EditorGUIUtility.singleLineHeight; // foldout

            //if (!property.isExpanded)
            //    return height;

            //SerializedProperty listProp = property.FindPropertyRelative("items");

            //height += 4; // margin

            //for (int i = 0; i < listProp.arraySize; i++)
            //{
            //    SerializedProperty element = listProp.GetArrayElementAtIndex(i);
            //    height += EditorGUI.GetPropertyHeight(element, includeChildren: true) + 2;
            //}

            //height += EditorGUIUtility.singleLineHeight + 8; // Add button

            //return height;
            var list = GetList(property);
            return list.GetHeight();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);

            //SerializedProperty listProp = property.FindPropertyRelative("items");

            //EditorGUI.BeginProperty(position, label, property);


            //property.isExpanded = EditorGUI.Foldout(
            //    new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
            //    property.isExpanded, label);

            //if (property.isExpanded)
            //{
            //    EditorGUI.indentLevel++;

            //    float y = position.y + EditorGUIUtility.singleLineHeight + 4;


            //    for (int i = 0; i < listProp.arraySize; i++)
            //    {
            //        SerializedProperty element = listProp.GetArrayElementAtIndex(i);

            //        float elementHeight = EditorGUI.GetPropertyHeight(element, true);

            //        Rect rect = new Rect(position.x, y, position.width - 30, elementHeight);

            //        EditorGUI.PropertyField(rect, element, GUIContent.none, true);


            //        if (GUI.Button(new Rect(rect.xMax + 5, y, 25, EditorGUIUtility.singleLineHeight), "X"))
            //        {
            //            listProp.DeleteArrayElementAtIndex(i);
            //        }

            //        y += elementHeight + 2;
            //    }

            //    float margin = 20.0f;
            //    var addRect = new Rect(position.x + margin, y + 4, position.width - margin * 2, EditorGUIUtility.singleLineHeight);

            //    if (GUI.Button(addRect, "Add " + elementType.ToString()))
            //    {
            //        GenericMenu menu = new GenericMenu();

            //        for (int i = 0; i < derivedTypes.Length; i++)
            //        {
            //            int idx = i;

            //            menu.AddItem(new GUIContent(derivedNames[i]), false, () =>
            //            {
            //                listProp.arraySize++;
            //                listProp.GetArrayElementAtIndex(listProp.arraySize - 1)
            //                    .managedReferenceValue = Activator.CreateInstance(derivedTypes[idx]);
            //                property.serializedObject.ApplyModifiedProperties();
            //            });
            //        }

            //        menu.ShowAsContext();
            //    }

            //    EditorGUI.indentLevel--;
            //}

            EditorGUI.BeginProperty(position, label, property);

            var list = GetList(property);

            // Set position and draw list
            list.DoList(position);

            EditorGUI.EndProperty();
        }
    }

}