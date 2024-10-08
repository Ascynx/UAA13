using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditor.EventSystems;

[CustomEditor(typeof(UseItem))]
public class UseItemVar : EventTriggerEditor
{
    //This method is called every time Unity needs to show or update the 
    //inspector when you select a gameobject with an XXX component.
    public override void OnInspectorGUI()
    {
        //Here the inspector will draw the default, generic inspector, which will show basically 
        //everything in a component, including the variables you want
        DrawDefaultInspector();
        //And then we will draw the inspector for event types and events, which is the default behaviour.
        base.OnInspectorGUI();
    }
}