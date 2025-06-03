using System.Collections;
using System.Collections.Generic;
using UnityEditor.EventSystems;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveEditorDelete))]
public class SaveEditorDeleteButtonEditor : EventTriggerEditor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        base.OnInspectorGUI();
    }
}
