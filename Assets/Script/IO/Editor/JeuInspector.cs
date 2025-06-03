using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Jeu))]
public class JeuInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Jeu jeu = (Jeu)target;
        EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);

        FicherSauvegarde fichier = jeu.fichierSauvegarde;


        if (fichier.Data != null)
        {
            Sauvegarde slot = fichier.Data;
            fichier.Data.Slot = EditorGUILayout.TextField("Slot Actuel", fichier.Data.Slot);
            if (GUILayout.Button("Sauvegarde"))
            {
                slot.SauvegardeFichier();
            }
            if (GUILayout.Button("Load"))
            {
                slot.LoadFichier();
            }
        }
    }
}
