using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsUI : MonoBehaviour
{
    public Text itemNameText; // Texte pour le nom de l'item
    public Text itemDescriptionText; // Texte pour la description de l'item
    public Image itemSpriteImage; // Image de l'item
    public Button equipButton; // Bouton pour �quiper/d�s�quiper
    public Button dropButton; // Bouton pour jeter l'item

    private Item currentItem; // Item actuellement affich�
    private Inventory inventory; // R�f�rence � l'inventaire

    // Affiche les d�tails d'un item
    public void DisplayItemDetails(Item item, Inventory inventory)
    {
        currentItem = item;
        this.inventory = inventory;

        // Met � jour l'interface utilisateur
        itemNameText.text = item.nom;
        itemDescriptionText.text = item.Description;
        itemSpriteImage.sprite = item.sprite;
        gameObject.SetActive(!gameObject.activeInHierarchy);
        // Configure le bouton �quiper/D�s�quiper
        if (IsEquipped(item))
        {
            equipButton.GetComponentInChildren<Text>().text = "D�s�quiper";
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(() => UnequipItem(item));
        }
        else
        {
            equipButton.GetComponentInChildren<Text>().text = "�quiper";
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(() => EquipItem(item));
        }

        // Configure le bouton Jeter
        dropButton.onClick.RemoveAllListeners();
        dropButton.onClick.AddListener(() => DropItem(item));
    }

    // V�rifie si l'item est �quip�
    public bool IsEquipped(Item item)
    {
        if (item is Sword && inventory.equippedSword == item) return true;
        if (item is Shield && inventory.equippedShield == item) return true;
        if (item is Parchemin && inventory.equippedParchemins.Contains((Parchemin)item)) return true;
        if (item is Relique && inventory.equippedRelique == item) return true;
        return false;
    }

    // �quipe un item
    private void EquipItem(Item item)
    {
        if (inventory.EquipItem(item))
        {
            DisplayItemDetails(item, inventory); // Rafra�chit l'interface
            inventory.RemoveItem(item);
        }
    }

    // D�s�quipe un item
    private void UnequipItem(Item item)
    {
        if (inventory.UnequipItem(item))
        {
            inventory.AddItem(item);
            DisplayItemDetails(item, inventory); // Rafra�chit l'interface
        }
    }

    // Jette un item
    private void DropItem(Item item)
    {
        inventory.RemoveItem(item);
        currentItem = null;
        gameObject.SetActive(false); // Cache l'interface des d�tails
    }
}
