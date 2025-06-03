using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EventSystems;
using UnityEngine;

[CustomEditor(typeof(SubGUIExitButton))]
public class SaveEditorExitButtonEditor : EventTriggerEditor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        base.OnInspectorGUI();
    }
}
