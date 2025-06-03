using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DictWrapper<TKey, TValue> : ISerializationCallbackReceiver
{

    private Dictionary<TKey, TValue> _dict = new();
    public Dictionary<TKey, TValue> Dictionary => _dict;

    [SerializeField]
    private List<KeyValue<TKey, TValue>> _data = new();

    public void OnAfterDeserialize()
    {
        _dict.Clear();
        foreach (KeyValue<TKey, TValue> kv in _data)
        {
            _dict.Add(kv.Key, kv.Value);
        }
    }

    public void OnBeforeSerialize()
    {
        _data.Clear();
        foreach (var kv in _dict)
        {
            _data.Add(new KeyValue<TKey, TValue> { Key = kv.Key, Value = kv.Value });
        }
    }

    public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
    {
        return _dict.GetEnumerator();
    }
}
