using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityState
{
    [SerializeField]
    public int instanceId;
    [SerializeField]
    public bool isActive;
    [SerializeField]
    public EntityType type;
    [SerializeField]
    public Vector3 position;


    public EntityState(int instanceId, bool isActive, EntityType type, Vector3 pos)
    {
        this.instanceId = instanceId;
        this.isActive = isActive;
        this.type = type;
        this.position = pos;
    }

    public enum EntityType
    {
        Mob,
        Item
    }
}
