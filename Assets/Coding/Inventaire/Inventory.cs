using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public List<ItemData> inventory = new List<ItemData>();
    public int inventoryLenght = 100;
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
            if(holderSlot.transform.childCount > 0)
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
                    slot.GetComponent<Image>().sprite = inventory[i].sprite;
                } else
                {
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.V) && inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(false);
        }
    }
}
