using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FicherSauvegarde))]
public class SauvegardeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FicherSauvegarde fichier = (FicherSauvegarde) target;

        string slot = "1";
        EditorGUILayout.TextField("Slot Actuel", slot);
        if (GUILayout.Button("Sauvegarde"))
        {
            fichier.SaveSauvegarde(slot).ContinueWith((v) => Debug.Log("Sauv� fichier slot: " + slot));
        }
        if (GUILayout.Button("Load"))
        {
            fichier.LoadSauvegarde(slot).ContinueWith((v) => Debug.Log("Charg� fichier slot: " + slot));
        }
    }
}