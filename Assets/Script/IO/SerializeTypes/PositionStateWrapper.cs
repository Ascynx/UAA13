using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionStateWrapper
{
    [SerializeField]
    private Vector2 _pos;

    public Vector2 Position
    {
        get { return _pos; }
        set { _pos = value; }
    }
}
