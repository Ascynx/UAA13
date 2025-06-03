using UnityEngine;
public class Item : ScriptableObject
{
    public string nom;
    public string Description;
    public Sprite sprite;

    public new virtual ItemType GetType()
    {
        return ItemType.Autre;
    }
}
[CreateAssetMenu(fileName = "Sword", menuName = "Items/New Sword")]
public class Sword : Item
{
    public Attaque attaque;

    public override ItemType GetType()
    {
        return ItemType.Sword;
    }
}
[CreateAssetMenu(fileName = "Shield", menuName = "Items/New Shield")]
public class Shield : Item
{
    public int def;

    public override ItemType GetType()
    {
        return ItemType.Shield;
    }
}
[CreateAssetMenu(fileName = "Parchemin", menuName = "Items/New Parchemin")]
public class Parchemin : Item
{
    public Attaque attaque;

    public override ItemType GetType()
    {
        return ItemType.Parchemin;
    }
}
[CreateAssetMenu(fileName = "Relique", menuName = "Items/New Relique")]
public class Relique : Item
{
    public int id;
    public Attaque attaque;

    public override ItemType GetType()
    {
        return ItemType.Relique;
    }
}

public enum ItemType
{
    Sword,
    Shield,
    Parchemin,
    Relique,
    Autre
}