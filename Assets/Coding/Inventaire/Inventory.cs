using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public List<ItemData> inventory = new List<ItemData>();
    public int inventoryLenght = 84;
    public GameObject inventoryPanel, holderSlot;
    private GameObject slot;
    public GameObject prefabs;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V) && !inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(true);
            if(holderSlot.transform.childCount < 0)
            {
                foreach (Transform item in holderSlot.transform)
                {
                    Destroy(item.gameObject);
                }
            }

            for (int i = 0; i < inventory.Count; i++)
            {
                slot = Instantiate(prefabs, transform.position, transform.rotation);
                slot.transform.SetParent(holderSlot.transform);

                if (inventory[i] != null )
                {
                    TextMeshProUGUI amount = slot.transform.Find("amout").GetComponent<TextMeshProUGUI>();
                    Image img = slot.transform.Find("icon").GetComponent<Image>();

                    amount.text = inventory[i].amout.ToString();
                    img.sprite = inventory[i].sprite;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.V) && inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(false);
        }
    }
}
