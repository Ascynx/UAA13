using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/New item")]
public class ItemData : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public GameObject prefab;
    public bool isStackable;
    public string amout;

    [System.Serializable]
    public enum Type
    {
        Sword, Shield, Parchemin, Relique, Rare
    }
    public Type _type;
}
