using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item; // Référence à l'item affiché dans le slot
    public Button slotButton; // Bouton du slot
    public Inventory inventory; // Référence à l'inventaire
    public ItemDetailsUI itemDetailsUI; // Interface des détails de l'item
    public InventoryUI inventoryUI;

    private void Start()
    {
        // Configure le bouton pour afficher les détails de l'item
        slotButton.onClick.AddListener(ShowItemDetails);
    }

    // Affiche les détails de l'item dans l'interface dédiée
    [SerializeField]
    int clicked = 0;
    [SerializeField]
    float clickTime = 0;
    [SerializeField]
    float clickDelay = 0.025f;
    private void ShowItemDetails()
    {
        clicked++;
        if (clicked == 1) clickTime = Time.time;


        if (clicked > 1 && Time.time - clickTime > clickDelay) 
        {
            clicked = 0;
            clickTime = 0;
            if (itemDetailsUI.IsEquipped(item))
            {
                //fonctionne pas /shrug
                inventory.UnequipItem(item);
                inventory.AddItem(item);
            } else
            {
                inventory.EquipItem(item);
                inventory.RemoveItem(item);
            }
            Debug.Log("Double Click");
            return;
        }

        if (item != null)
        {
            itemDetailsUI.DisplayItemDetails(item, inventory);
            inventoryUI.ReloadUI();
        }

        if (clicked > 2 || Time.time - clickTime > 1) clicked = 0;
    }
}
