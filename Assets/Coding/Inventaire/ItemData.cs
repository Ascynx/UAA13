using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/New item")]
public class ItemData : ScriptableObject
{
    public string nom;
    public string Description;
    public Sprite sprite;

}

[CreateAssetMenu(fileName = "Sword", menuName = "Items/New Sword")]
public class Sword : ItemData
{
    public Attaque attaque;
}

[CreateAssetMenu(fileName = "Shield", menuName = "Items/New Shield")]
public class Shield : ItemData
{
    public int def;
}

[CreateAssetMenu(fileName = "Parchemin", menuName = "Items/New Parchemin")]
public class Parchemin : ItemData
{
    public Attaque attaque;
}

[CreateAssetMenu(fileName = "Relique", menuName = "Items/New Relique")]
public class Relique : ItemData
{
    public int id;
}