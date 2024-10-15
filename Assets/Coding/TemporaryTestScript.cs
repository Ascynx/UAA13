using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TemporaryTestScript : MonoBehaviour
{
    private static FicherSauvegarde sauvegarde;

    void Awake()
    {
        Debug.Log("Test script: creating and saving sauvegarde");
        sauvegarde = FicherSauvegarde.GetInstance();
        sauvegarde.SetChildData(this.AddComponent<Sauvegarde>());
        sauvegarde.Data.test();
        Debug.Log("Test script: Done");
    }
}
