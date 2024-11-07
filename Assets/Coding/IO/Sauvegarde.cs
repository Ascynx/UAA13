using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Sauvegarde : MonoBehaviour
{
    private FicherSauvegarde _parent;

    [SerializeField]
    private string _slot = "UNKNOWN";

    public void SetParent(FicherSauvegarde parent)
    {
        this._parent = parent;
    }

    [SerializeField]
    private DictWrapper<string, bool> _events = new();

    public FicherSauvegarde Parent { get { return _parent; } }
    public DictWrapper<string, bool> Events { get {  return _events; } }
    public string Slot { get { return _slot; } set { _slot = value; } }


    public void SauvegardeFichier()
    {
        SauvegardeFichier(this._slot);
    }

    public void SauvegardeFichier(string slot)
    {
        _parent.SaveSauvegarde(slot);
    }

    public void LoadFichier()
    {
        LoadFichier(this._slot);
    }

    public void LoadFichier(string slot)
    {
        _parent.LoadSauvegarde(slot);
    }
}
