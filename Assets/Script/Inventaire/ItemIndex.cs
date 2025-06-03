using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndex : MonoBehaviour
{
    private void Awake()
    {
        //le warning pour le game object manquant est un problème sur l'item et non pas de ce code-ci,
        //vérifie les items avant de venir modifier ici. 
        Item[] resources = Resources.LoadAll<Item>("Item");
        for (int i = 0; i < resources.Length; i++)
        {
            Item item = resources[i];
            _items.Add(GetRefFromName(item.nom), item);
        }
    }

    private Dictionary<string, Item> _items = new();

    public void Register(string name, Item item)
    {
        if (_items.ContainsKey(name))
        {
            Debug.LogError($"Item with name {name} already registered.");
            return;
        }
        _items.Add(name, item);
    }

    public void Unregister(string name)
    {
        if (!_items.ContainsKey(name))
        {
            Debug.LogError($"Item with name {name} not found.");
            return;
        }
        _items.Remove(name);
    }

    public void Clear()
    {
        _items.Clear();
    }

#nullable enable
    public Item? GetItemFromReference(string name)
    {
        string reference = GetRefFromName(name);
        if (!_items.TryGetValue(reference, out Item item))
        {
            Debug.LogError($"Item with name {name} ({reference}) not found.");
            #pragma warning disable CS8600
            item = null;
            #pragma warning restore CS8600
        }
        return item;
    }

    public string GetRefFromName(string name)
    {
        return name.Replace(" ", "_").ToLowerInvariant();
    }

    public override string ToString()
    {
        string[] keys = new string[_items.Keys.Count];
        _items.Keys.CopyTo(keys, 0);

        string v = "";
        for (int i = 0; i < _items.Count; i++)
        {
            string key = keys[i];
            Item item = _items[key];
            v += $"Item {i}: {key} - {item.nom}\n";
        }
        return v;
    }
}
