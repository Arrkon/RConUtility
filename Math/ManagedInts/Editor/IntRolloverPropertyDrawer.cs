//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
//  RConApps		2022-08-12 7:20:14 PM
//	RConUtility
//  IntRolloverPropertyDrawer
//  Custom property drawer for IntRollover
//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR

using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

namespace Editor
{
    [CustomPropertyDrawer(typeof(RConUtility.Math.IntRollover))]
    public class IntRolloverPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if(property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight * 3;
            }
            else
            {
                return EditorGUIUtility.singleLineHeight;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property);
            position.height = EditorGUIUtility.singleLineHeight;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);
            if(property.isExpanded)
            {
                var min = property.FindPropertyRelative("_min");
                var val = property.FindPropertyRelative("_value");
                var max = property.FindPropertyRelative("_max");

                float y = position.y + EditorGUIUtility.singleLineHeight;
                float x = position.x;
                float height = EditorGUIUtility.singleLineHeight;
                float width = position.width / 4 - 5;
                Vector2 size = new Vector2(width, height);


                Vector2 minPos = new Vector2(x, y);
                Rect minRect = new Rect(minPos, size);
                x = position.width / 3;
                Vector2 valPos = new Vector2(x, y);
                Rect valRect = new Rect(valPos, size);
                x = position.width * 2 / 3;
                Vector2 maxPos = new Vector2(x, y);
                Rect maxRect = new Rect(maxPos, size);

                EditorGUI.LabelField(minRect, "Min");
                EditorGUI.LabelField(valRect, "Value");
                EditorGUI.LabelField(maxRect, "Max");

                y += EditorGUIUtility.singleLineHeight;
                x = position.x;
                minPos = new Vector2(x, y);
                minRect = new Rect(minPos, size);
                x = position.width / 3;
                valPos = new Vector2(x, y);
                valRect = new Rect(valPos, size);
                x = position.width * 2 / 3;
                maxPos = new Vector2(x, y);
                maxRect = new Rect(maxPos, size);

                int minimum = EditorGUI.IntField(minRect, min.intValue);
                int value = EditorGUI.IntField(valRect, val.intValue);
                int maximum = EditorGUI.IntField(maxRect, max.intValue);

                // keep value in bounds
                val.intValue = Mathf.Min(Mathf.Max(value, min.intValue), max.intValue);

                // keep min the lowest and max the highest
                if(minimum != min.intValue)
                {
                    minimum = Mathf.Min(minimum, maximum);
                    min.intValue = minimum;
                }
                if(maximum != max.intValue)
                {
                    maximum = Mathf.Max(minimum, maximum);
                    max.intValue = maximum;
                }
            }
            EditorGUI.EndProperty();
        }
    }
}