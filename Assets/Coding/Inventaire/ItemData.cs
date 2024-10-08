using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/New item")]
public class ItemData : ScriptableObject
{
    public string nom;
    public Sprite sprite;
    public GameObject prefab;
    public bool isStackable;
    public string amout;

    [System.Serializable]
    public enum classe
    {
        Sword, 
        Shield, 
        Parchemin, 
        Relique, 
        Commun
    }
    public classe type;
}
