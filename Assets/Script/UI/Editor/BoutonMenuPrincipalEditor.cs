using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EventSystems;
using UnityEngine;

[CustomEditor(typeof(BoutonMenuPrincipal))]
public class BoutonMenuPrincipalEditor : EventTriggerEditor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        base.OnInspectorGUI();
    }
}
