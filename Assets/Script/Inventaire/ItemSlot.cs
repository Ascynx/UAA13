using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item; // R�f�rence � l'item affich� dans le slot
    public Button slotButton; // Bouton du slot
    public Inventory inventory; // R�f�rence � l'inventaire
    public ItemDetailsUI itemDetailsUI; // Interface des d�tails de l'item

    private void Start()
    {
        // Configure le bouton pour afficher les d�tails de l'item
        slotButton.onClick.AddListener(ShowItemDetails);
    }

    // Affiche les d�tails de l'item dans l'interface d�di�e
    private void ShowItemDetails()
    {
        if (item != null)
        {
            itemDetailsUI.DisplayItemDetails(item, inventory);
        }
    }
}
