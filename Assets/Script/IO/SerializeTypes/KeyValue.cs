using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;

[Serializable]
public class KeyValue<TKey, TValue>
{
    public TKey Key;
    public TValue Value;
}
