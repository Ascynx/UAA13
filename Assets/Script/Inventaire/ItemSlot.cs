using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item; // Référence à l'item affiché dans le slot
    public Button slotButton; // Bouton du slot
    public Inventory inventory; // Référence à l'inventaire
    public ItemDetailsUI itemDetailsUI; // Interface des détails de l'item

    private void Start()
    {
        // Configure le bouton pour afficher les détails de l'item
        slotButton.onClick.AddListener(ShowItemDetails);
    }

    // Affiche les détails de l'item dans l'interface dédiée
    private void ShowItemDetails()
    {
        if (item != null)
        {
            itemDetailsUI.DisplayItemDetails(item, inventory);
        }
    }
}
