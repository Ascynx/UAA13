using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DictWrapper<TKey, TValue> : ISerializationCallbackReceiver
{
    [Serializable]
    class KeyValue
    {
        public TKey Key;
        public TValue Value;
    }

    private Dictionary<TKey, TValue> _dict = new();
    public Dictionary<TKey, TValue> Dictionary => _dict;

    [SerializeField]
    private List<KeyValue> _data = new();

    public void OnAfterDeserialize()
    {
        _dict.Clear();
        foreach (KeyValue kv in _data)
        {
            _dict.Add(kv.Key, kv.Value);
        }
    }

    public void OnBeforeSerialize()
    {
        _data.Clear();
        foreach (var kv in _dict)
        {
            _data.Add(new KeyValue { Key = kv.Key, Value = kv.Value });
        }
    }
}
