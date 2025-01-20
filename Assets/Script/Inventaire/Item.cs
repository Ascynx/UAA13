using UnityEngine;
public class Item : ScriptableObject
{
    public string nom;
    public string Description;
    public Sprite sprite;
}
[CreateAssetMenu(fileName = "Sword", menuName = "Items/New Sword")]
public class Sword : Item
{
    public Attaque attaque;
}
[CreateAssetMenu(fileName = "Shield", menuName = "Items/New Shield")]
public class Shield : Item
{
    public int def;
}
[CreateAssetMenu(fileName = "Parchemin", menuName = "Items/New Parchemin")]
public class Parchemin : Item
{
    public Attaque attaque;
}
[CreateAssetMenu(fileName = "Relique", menuName = "Items/New Relique")]
public class Relique : Item
{
    public int id;
    public Attaque attaque;
}