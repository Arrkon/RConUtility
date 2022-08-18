//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
//  RConApps		2022-08-12 10:53:43 PM
//	RConUtility
//  IntVector2PropertyDrawer
//  Custome property drawer for IVector2
//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR

using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

namespace Editor
{
    [CustomPropertyDrawer(typeof(RConUtility.Math.IntVector3))]
    public class IVector3PropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUIUtility.singleLineHeight;
            if(!EditorGUIUtility.wideMode)
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUIContent[] subLabels = { new GUIContent("X"), new GUIContent("Y") };
            var startIter = "x";

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.MultiPropertyField(position, subLabels, property.FindPropertyRelative(startIter), label);
            EditorGUI.EndProperty();
        }
    }
}