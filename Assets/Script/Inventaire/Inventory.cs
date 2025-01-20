using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // Liste des items dans l'inventaire
    public int maxSlots = 20; // Nombre maximum d'items dans l'inventaire

    // Emplacements d'équipement
    public Sword equippedSword;
    public Shield equippedShield;
    public List<Parchemin> equippedParchemins = new List<Parchemin>();
    public Relique equippedRelique;

    // Ajoute un item à l'inventaire
    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventaire plein !");
            return false;
        }

        items.Add(item);
        Debug.Log($"{item.nom} ajouté à l'inventaire.");
        return true;
    }

    // Supprime un item de l'inventaire
    public bool RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"{item.nom} retiré de l'inventaire.");
            return true;
        }

        Debug.Log("L'item n'est pas dans l'inventaire !");
        return false;
    }

    // Équipe un item
    public bool EquipItem(Item item)
    {
        if (item is Sword sword)
        {
            equippedSword = sword;
            Debug.Log($"Équipé l'épée : {sword.nom}");
        }
        else if (item is Shield shield)
        {
            equippedShield = shield;
            Debug.Log($"Équipé le bouclier : {shield.nom}");
        }
        else if (item is Parchemin parchemin)
        {
            if (equippedParchemins.Count < 3)
            {
                equippedParchemins.Add(parchemin);
                Debug.Log($"Équipé un parchemin : {parchemin.nom}");
            }
            else
            {
                Debug.Log("Vous ne pouvez équiper que 3 parchemins !");
                return false;
            }
        }
        else if (item is Relique relique)
        {
            equippedRelique = relique;
            Debug.Log($"Équipé la relique : {relique.nom}");
        }
        else
        {
            Debug.Log("Cet item ne peut pas être équipé !");
            return false;
        }
        return true;
    }

    // Déséquipe un item
    public bool UnequipItem(Item item)
    {
        if (item == equippedSword)
        {
            equippedSword = null;
            Debug.Log("Épée déséquipée.");
        }
        else if (item == equippedShield)
        {
            equippedShield = null;
            Debug.Log("Bouclier déséquipé.");
        }
        else if (item is Parchemin parchemin && equippedParchemins.Contains(parchemin))
        {
            equippedParchemins.Remove(parchemin);
            Debug.Log($"Parchemin {parchemin.nom} déséquipé.");
        }
        else if (item == equippedRelique)
        {
            equippedRelique = null;
            Debug.Log("Relique déséquipée.");
        }
        else
        {
            Debug.Log("Cet item n'est pas équipé !");
            return false;
        }
        return true;
    }
}