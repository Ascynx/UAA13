using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // Liste des items dans l'inventaire
    public int maxSlots = 20; // Nombre maximum d'items dans l'inventaire

    // Emplacements d'�quipement
    public Sword equippedSword;
    public Shield equippedShield;
    public List<Parchemin> equippedParchemins = new List<Parchemin>();
    public Relique equippedRelique;

    // Ajoute un item � l'inventaire
    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventaire plein !");
            return false;
        }

        items.Add(item);
        Debug.Log($"{item.nom} ajout� � l'inventaire.");
        return true;
    }

    // Supprime un item de l'inventaire
    public bool RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"{item.nom} retir� de l'inventaire.");
            return true;
        }

        Debug.Log("L'item n'est pas dans l'inventaire !");
        return false;
    }

    // �quipe un item
    public bool EquipItem(Item item)
    {
        if (item is Sword sword)
        {
            equippedSword = sword;
            Debug.Log($"�quip� l'�p�e : {sword.nom}");
        }
        else if (item is Shield shield)
        {
            equippedShield = shield;
            Debug.Log($"�quip� le bouclier : {shield.nom}");
        }
        else if (item is Parchemin parchemin)
        {
            if (equippedParchemins.Count < 3)
            {
                equippedParchemins.Add(parchemin);
                Debug.Log($"�quip� un parchemin : {parchemin.nom}");
            }
            else
            {
                Debug.Log("Vous ne pouvez �quiper que 3 parchemins !");
                return false;
            }
        }
        else if (item is Relique relique)
        {
            equippedRelique = relique;
            Debug.Log($"�quip� la relique : {relique.nom}");
        }
        else
        {
            Debug.Log("Cet item ne peut pas �tre �quip� !");
            return false;
        }
        return true;
    }

    // D�s�quipe un item
    public bool UnequipItem(Item item)
    {
        if (item == equippedSword)
        {
            equippedSword = null;
            Debug.Log("�p�e d�s�quip�e.");
        }
        else if (item == equippedShield)
        {
            equippedShield = null;
            Debug.Log("Bouclier d�s�quip�.");
        }
        else if (item is Parchemin parchemin && equippedParchemins.Contains(parchemin))
        {
            equippedParchemins.Remove(parchemin);
            Debug.Log($"Parchemin {parchemin.nom} d�s�quip�.");
        }
        else if (item == equippedRelique)
        {
            equippedRelique = null;
            Debug.Log("Relique d�s�quip�e.");
        }
        else
        {
            Debug.Log("Cet item n'est pas �quip� !");
            return false;
        }
        return true;
    }
}