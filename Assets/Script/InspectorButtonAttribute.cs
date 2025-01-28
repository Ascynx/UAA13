using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class InspectorButtonAttribute : PropertyAttribute
{
    public static readonly float defaultButtonWidth = 80;

    public readonly string MethodName;

    private float _buttonWidth = defaultButtonWidth;
    public float ButtonWidth
    {
        get { return _buttonWidth; }
        set { _buttonWidth = value; }
    }

    public InspectorButtonAttribute(string methodName)
    {
        MethodName = methodName;
    }
    
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorButtonPropertyDrawer  : PropertyDrawer
{
    private MethodInfo _methodInfo = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        InspectorButtonAttribute attr = (InspectorButtonAttribute)attribute;
        GUI.enabled = Application.isPlaying;

        Rect buttonRect = new Rect(position.x + (position.width - attr.ButtonWidth) * 0.5f, position.y, attr.ButtonWidth, position.height);
        if (GUI.Button(buttonRect, label))
        {
            Type ownerType = property.serializedObject.targetObject.GetType();
            if (_methodInfo == null)
            {
                _methodInfo = ownerType.GetMethod(attr.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }

            if (_methodInfo != null)
            {
                _methodInfo.Invoke(property.serializedObject.targetObject, null);
            } else
            {
                Debug.LogWarning($"InspectorButton: Unable to find method {attr.MethodName} in {ownerType}");
            }
        }
    }
}
#endif
