using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform inventoryPanel; // Panel pour les items dans l'inventaire
    public Transform equipmentPanel; // Panel pour les équipements
    public Transform DetailsPanel; // Panel pour les équipements
    public Transform DetailsTemplate; // Panel pour les équipements
    public GameObject itemSlotPrefab; // Préfabriqué d'un slot d'item
    public ItemDetailsUI itemDetailsUI; // Interface des détails de l'item

    public void ShowPanel()
    {
        UpdateUI();
        inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeInHierarchy);
        equipmentPanel.gameObject.SetActive(!equipmentPanel.gameObject.activeInHierarchy);
        DetailsPanel = DetailsTemplate;
        DetailsPanel.gameObject.SetActive(equipmentPanel.gameObject.activeInHierarchy);
    }

    public void UpdateUI()
    {
        // Met à jour l'inventaire
        UpdateInventoryUI();

        // Met à jour les équipements
        UpdateEquipmentUI();
    }

    private void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in inventory.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, inventoryPanel);
            var itemSlot = slot.GetComponent<ItemSlot>();
            itemSlot.item = item;
            itemSlot.inventory = inventory;
            itemSlot.itemDetailsUI = itemDetailsUI;

            slot.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
        }
    }

    private void UpdateEquipmentUI()
    {
        foreach (Transform child in equipmentPanel)
        {
            Destroy(child.gameObject);
        }

        if (inventory.equippedSword != null)
            CreateEquipmentSlot(inventory.equippedSword);

        if (inventory.equippedShield != null)
            CreateEquipmentSlot(inventory.equippedShield);

        foreach (var parchemin in inventory.equippedParchemins)
        {
            CreateEquipmentSlot(parchemin);
        }

        if (inventory.equippedRelique != null)
            CreateEquipmentSlot(inventory.equippedRelique);
    }

    private void CreateEquipmentSlot(Item item)
    {
        GameObject slot = Instantiate(itemSlotPrefab, equipmentPanel);
        var itemSlot = slot.GetComponent<ItemSlot>();
        itemSlot.item = item;
        itemSlot.inventory = inventory;
        itemSlot.itemDetailsUI = itemDetailsUI;

        slot.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
    }
}
