using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FicherSauvegarde))]
public class SauvegardeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FicherSauvegarde fichier = (FicherSauvegarde)target;
        if (fichier.Data != null)
        {
            string slot = fichier.Data.Slot;
            fichier.Data.Slot = EditorGUILayout.TextField("Slot Actuel", fichier.Data.Slot);
            if (GUILayout.Button("Sauvegarde"))
            {
                fichier.SaveSauvegarde(slot).ContinueWith((v) => Debug.Log("Sauvé fichier slot: " + slot));
            }
            if (GUILayout.Button("Load"))
            {
                fichier.LoadSauvegarde(slot).ContinueWith((v) => Debug.Log("Chargé fichier slot: " + slot));
            }
        }
    }
}