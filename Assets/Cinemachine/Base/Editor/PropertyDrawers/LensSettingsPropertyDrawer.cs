using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using Object = UnityEngine.Object;

namespace Cinemachine.Editor
{
    [CustomPropertyDrawer(typeof(LensSettingsPropertyAttribute))]
    public sealed  class LensSettingsPropertyDrawer : PropertyDrawer
    {
        const int vSpace = 2;
        bool mExpanded = true;
        LensSettings def = new LensSettings(); // to access name strings

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;
            rect.height = height;
            mExpanded = EditorGUI.Foldout(rect, mExpanded, label);
            if (mExpanded)
            {
                bool ortho = false;
                PropertyInfo pi = typeof(LensSettings).GetProperty(
                    "Orthographic", BindingFlags.NonPublic | BindingFlags.Instance);
                if (pi != null)
                    ortho = bool.Equals(true, pi.GetValue(GetPropertyValue(property), null));

                ++EditorGUI.indentLevel;
                rect.y += height + vSpace;
                if (ortho)
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative(() => def.OrthographicSize));
                else
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative(() => def.FieldOfView));
                rect.y += height + vSpace;
                EditorGUI.PropertyField(rect, property.FindPropertyRelative(() => def.NearClipPlane));
                rect.y += height + vSpace;
                EditorGUI.PropertyField(rect, property.FindPropertyRelative(() => def.FarClipPlane));
                rect.y += height + vSpace;
                EditorGUI.PropertyField(rect, property.FindPropertyRelative(() => def.Dutch));
                --EditorGUI.indentLevel;
            }
        }

        object GetPropertyValue(SerializedProperty property)
        {
            Object targetObject = property.serializedObject.targetObject;
            Type targetObjectClassType = targetObject.GetType();
            FieldInfo field = targetObjectClassType.GetField(property.propertyPath);
            if (field != null)
                return field.GetValue(targetObject);
            return null;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight + vSpace;
            if (mExpanded)
                height *= 5;
            return height;
        }
    }
}
