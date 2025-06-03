using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsUI : MonoBehaviour
{
    public Text itemNameText; // Texte pour le nom de l'item
    public Text itemDescriptionText; // Texte pour la description de l'item
    public Image itemSpriteImage; // Image de l'item
    public Button equipButton; // Bouton pour équiper/déséquiper
    public Button dropButton; // Bouton pour jeter l'item

    private Item currentItem; // Item actuellement affiché
    private Inventory inventory; // Référence à l'inventaire

    // Affiche les détails d'un item
    public void DisplayItemDetails(Item item, Inventory inventory)
    {
        currentItem = item;
        this.inventory = inventory;

        // Met à jour l'interface utilisateur
        itemNameText.text = item.nom;
        itemDescriptionText.text = item.Description;
        itemSpriteImage.sprite = item.sprite;
        gameObject.SetActive(!gameObject.activeInHierarchy);
        // Configure le bouton Équiper/Déséquiper
        if (IsEquipped(item))
        {
            equipButton.GetComponentInChildren<Text>().text = "Déséquiper";
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(() => UnequipItem(item));
        }
        else
        {
            equipButton.GetComponentInChildren<Text>().text = "Équiper";
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(() => EquipItem(item));
        }

        // Configure le bouton Jeter
        dropButton.onClick.RemoveAllListeners();
        dropButton.onClick.AddListener(() => DropItem(item));
    }

    // Vérifie si l'item est équipé
    public bool IsEquipped(Item item)
    {
        if (item is Sword && inventory.equippedSword == item) return true;
        if (item is Shield && inventory.equippedShield == item) return true;
        if (item is Parchemin && inventory.equippedParchemins.Contains((Parchemin)item)) return true;
        if (item is Relique && inventory.equippedRelique == item) return true;
        return false;
    }

    // Équipe un item
    private void EquipItem(Item item)
    {
        if (inventory.EquipItem(item))
        {
            DisplayItemDetails(item, inventory); // Rafraîchit l'interface
            inventory.RemoveItem(item);
        }
    }

    // Déséquipe un item
    private void UnequipItem(Item item)
    {
        if (inventory.UnequipItem(item))
        {
            inventory.AddItem(item);
            DisplayItemDetails(item, inventory); // Rafraîchit l'interface
        }
    }

    // Jette un item
    private void DropItem(Item item)
    {
        inventory.RemoveItem(item);
        currentItem = null;
        gameObject.SetActive(false); // Cache l'interface des détails
    }
}
