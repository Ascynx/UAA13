using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapTied
{
    [SerializeField]
    private int _physicalLayerId;
    [SerializeField]
    private int _layerId;

    public int LayerId { get { return _layerId; } set { _layerId = value; } }
    public int PhysicalLayerId { get { return _physicalLayerId; } set { _physicalLayerId = value; } }

    public MapTied(int physicalLayer, int layer)
    {
        _physicalLayerId = physicalLayer;
        _layerId = layer;
    }
}
