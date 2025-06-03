using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : AbstractGUI
{
    public Inventory inventory;
    public Transform inventoryPanel; // Panel pour les items dans l'inventaire
    public Transform equipmentPanel; // Panel pour les �quipements
    public Transform DetailsPanel; // Panel pour les �quipements
    public Transform DetailsTemplate; // Panel pour les �quipements
    public GameObject itemSlotPrefab; // Pr�fabriqu� d'un slot d'item
    public ItemDetailsUI itemDetailsUI; // Interface des d�tails de l'item


    public override void OnCloseGui()
    {
        inventoryPanel.gameObject.SetActive(false);
        equipmentPanel.gameObject.SetActive(false);
        DetailsPanel.gameObject.SetActive(false);
    }

    public override void OnOpenGui()
    {
        ReloadUI();
        inventoryPanel.gameObject.SetActive(true);
        equipmentPanel.gameObject.SetActive(true);
        DetailsPanel.gameObject.SetActive(true);
    }

    private void SetStates(bool newState)
    {
        inventoryPanel.gameObject.SetActive(newState);
        equipmentPanel.gameObject.SetActive(newState);
        DetailsPanel.gameObject.SetActive(newState);
    }

    public bool ShowPanel()
    {
        bool etaitInactif = LoadUnloadUI();
        System.Threading.Thread.Sleep(100);
        return etaitInactif;
    }

    public bool LoadUnloadUI()
    {
        UpdateUI();
        DetailsPanel = DetailsTemplate;
        SetStates(!inventoryPanel.gameObject.activeInHierarchy);
        return !inventoryPanel.gameObject.activeInHierarchy;
    }

    public void ReloadUI()
    {
        UpdateUI();
        DetailsPanel = DetailsTemplate;
    }

    public void UpdateUI()
    {
        // Met � jour l'inventaire
        UpdateInventoryUI();

        // Met � jour les �quipements
        UpdateEquipmentUI();
    }

    private void UpdateInventoryUI()
    {
        if (inventoryPanel.childCount > inventory.items.Count)
        {
            //clear overflow items.
            for (int i = inventory.items.Count; i < inventoryPanel.childCount; i++)
            {
                Destroy(inventoryPanel.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < inventory.items.Count; i++)
        {
            Item item = inventory.items[i];
            if (i < inventoryPanel.childCount)
            {
                for (int j = i; j < inventoryPanel.childCount; j++)
                {
                    if (inventoryPanel.GetChild(j).GetComponent<ItemSlot>().item == item)
                    {
                        break;
                    } else
                    {
                        ItemSlot slot = inventoryPanel.GetChild(j).GetComponent<ItemSlot>();
                        slot.item = item;
                        slot.inventory = inventory;
                        slot.itemDetailsUI = itemDetailsUI;

                        slot.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
                    }
                }
            } else
            {
                GameObject slot = Instantiate(itemSlotPrefab, inventoryPanel);
                var itemSlot = slot.GetComponent<ItemSlot>();
                itemSlot.item = item;
                itemSlot.inventory = inventory;
                itemSlot.itemDetailsUI = itemDetailsUI;
                slot.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
            }
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

    public override void OnGuiMove(Vector2 dir)
    {
    }

    public override void OnGuiSelect()
    {
    }

    public override bool CanBeEscaped()
    {
        return true;
    }

    public override void OnSubGuiClosed()
    {
    }

    public override void OnSubGuiOpen()
    {
    }
}
