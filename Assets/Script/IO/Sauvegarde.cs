using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

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
    [SerializeField]
    private DictWrapper<string, QuestObjective> _objectives = new();

    [SerializeField]
    private PositionStateWrapper _playerPositionState = new();
    [SerializeField]
    private MapTied _playerMap = new(6, 9);

    [SerializeField]
    private List<Item> _items = new();
    [SerializeField]
    private Shield _shield = null;
    [SerializeField]
    private Sword _epee = null;
    [SerializeField]
    private List<Parchemin> _parchemins = new();
    [SerializeField]
    private int maxSlots = 20;
    [SerializeField]
    private Relique _relique = null;

    [SerializeField]
    private List<EntityState> _PersistentEntityStates = new();

    public FicherSauvegarde Parent { get { return _parent; } }
    public DictWrapper<string, bool> Events { get { return _events; } }
    public DictWrapper<string, QuestObjective> Objectives { get { return _objectives; } }

    public PositionStateWrapper PlayerPositionState { get { return _playerPositionState; } }
    public MapTied PlayerMap { get { return _playerMap; } }

    public List<Item> Items { get { return _items; } set { _items = value; } }
    public Shield Shield { get { return _shield; } set { _shield = value; } }
    public Sword Epee { get { return _epee; } set { _epee = value; } }
    public List<Parchemin> Parchemins { get { return _parchemins; } set { _parchemins = value; } }

    public int MaxSlots { get { return maxSlots; } set { maxSlots = value; } }
    public Relique Relique { get { return _relique; } set { _relique = value; } }

    public List<EntityState> PersistentEntityStates { get { return _PersistentEntityStates; } set { _PersistentEntityStates = value; } }


    public string Slot { get { return _slot; } set { _slot = value; } }


    public void SauvegardeFichier()
    {
        SauvegardeFichier(this._slot);
    }

    public void SauvegardeFichier(string slot)
    {
        PrepareSave();
        _parent.SaveSauvegarde(slot);
    }

    public void LoadFichier()
    {
        LoadFichier(this._slot);

    }

    public void LoadFichier(string slot)
    {
        _parent.LoadSauvegarde(slot);
        LoadFromData();
    }

    public void PrepareSave()
    {
        Jeu.Instance.gameObject.SendMessage("OnPreSave", this, SendMessageOptions.DontRequireReceiver);
    }

    public void LoadFromData()
    {
        Jeu.Instance.gameObject.SendMessage("OnPostLoad", this, SendMessageOptions.DontRequireReceiver);
    }

    public void CopyTo(ref Sauvegarde other)
    {
        other._events = new DictWrapper<string, bool>();
        foreach (var kvp in _events)
        {
            other._events.Dictionary.Add(kvp.Key, kvp.Value);
        }

        other._objectives = new DictWrapper<string, QuestObjective>();
        foreach (var kvp in _objectives)
        {
            other._objectives.Dictionary.Add(kvp.Key, kvp.Value);
        }

        other._playerPositionState = new PositionStateWrapper
        {
            Position = _playerPositionState.Position
        };

        other._playerMap = new MapTied(_playerMap.PhysicalLayerId, _playerMap.LayerId);
        other._items = new List<Item>(_items);
        other._shield = _shield;
        other._epee = _epee;
        other._parchemins = new List<Parchemin>(_parchemins);
        other.maxSlots = maxSlots;
        other._relique = _relique;
        other._PersistentEntityStates = new List<EntityState>(_PersistentEntityStates);
    }


    public bool FichierExiste(string slot)
    {
        return _parent.VerifieSauvegarde(slot);
    }

    public void DeleteFichier(string slot)
    {
        _parent.DeleteSauvegarde(slot);
    }
}
