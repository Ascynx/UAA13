using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EventSystems;
using UnityEngine;

[CustomEditor(typeof(PauseMenuButton))]
public class PauseMenuButtonEditor : EventTriggerEditor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        base.OnInspectorGUI();
    }
}
