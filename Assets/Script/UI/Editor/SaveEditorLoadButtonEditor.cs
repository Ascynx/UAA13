using System.Collections;
using System.Collections.Generic;
using UnityEditor.EventSystems;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveEditorLoad))]
public class SaveEditorLoadButtonEditor : EventTriggerEditor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        base.OnInspectorGUI();
    }
}
