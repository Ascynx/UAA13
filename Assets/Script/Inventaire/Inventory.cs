using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Inventory : MonoBehaviour
{
    public Canvas inventoryCanvas;

    [SerializeField]
    public List<Item> items = new List<Item>(); // Liste des items dans l'inventaire
    [SerializeField]
    public int maxSlots = 20; // Nombre maximum d'items dans l'inventaire
    // Emplacements d'�quipement
    [SerializeField]
    public Sword equippedSword;
    [SerializeField]
    public Shield equippedShield;
    [SerializeField]
    public List<Parchemin> equippedParchemins = new List<Parchemin>();
    [SerializeField]
    public Relique equippedRelique;


    public InventoryUI inventoryUI;

    private void Awake()
    {
    }

    public void OnOpenInventory(InputValue value)
    {
        if (Jeu.Instance.openedGUI is InventoryUI)
        {
            Jeu.Instance.CloseGUI();
        } else
        {
            Jeu.Instance.OpenGUI(inventoryCanvas.GetComponent<InventoryUI>());
        }
    }

    public void OnPreSave(Sauvegarde save)
    {
        save.Items = items;
        save.Shield = equippedShield;
        save.Epee = equippedSword;
        save.Parchemins = equippedParchemins;
        save.MaxSlots = maxSlots;
        save.Relique = equippedRelique;
    }

    public void OnPostLoad(Sauvegarde save)
    {
        items = save.Items;
        equippedShield = save.Shield;
        equippedSword = save.Epee;
        equippedParchemins = save.Parchemins;
        maxSlots = save.MaxSlots;
        equippedRelique = save.Relique;
        inventoryUI.ReloadUI();
    }

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
            System.Threading.Thread.Sleep(100);
            inventoryUI.ReloadUI();
            return true;
        }

        Debug.Log("L'item n'est pas dans l'inventaire !");
        return false;
    }

    // �quipe un item
    public bool EquipItem(Item item)
    {
        if (item is Sword sword && equippedSword == null)
        {
            equippedSword = sword;
            Debug.Log($"�quip� l'�p�e : {sword.nom}");
        }
        else if (item is Shield shield && equippedShield == null)
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
        else if (item is Relique relique && equippedRelique == null)
        {
            equippedRelique = relique;
            Debug.Log($"�quip� la relique : {relique.nom}");
        }
        else
        {
            Debug.Log("Cet item ne peut pas �tre �quip� !");
            return false;
        }
        System.Threading.Thread.Sleep(100);
        inventoryUI.ReloadUI();
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
        System.Threading.Thread.Sleep(100);
        inventoryUI.ReloadUI();
        return true;
    }


}