using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public List<ItemData> inventory = new List<ItemData>();
    public int inventoryLenght = 100;
    public GameObject inventoryPanel, holderSlot, slot, prefabs;

    public Sword sword;
    public Shield shield;
    public Parchemin parchemin1, parchemin2, parchemin3;
    public Relique relique;

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

            if (sword != null)
            {
                slot = Instantiate(prefabs, transform.position, transform.rotation);
                slot.transform.SetParent(holderSlot.transform);
                slot.GetComponent<TextMeshProUGUI>().text = sword.nom + " - Épée active";
                slot.GetComponent<UseItem>().item = sword;
                slot.transform.name = "itemTMP sword";
            }
            if (shield != null)
            {
                slot = Instantiate(prefabs, transform.position, transform.rotation);
                slot.transform.SetParent(holderSlot.transform);
                slot.GetComponent<TextMeshProUGUI>().text = sword.nom + " - Bouclier actif";
                slot.GetComponent<UseItem>().item = shield;
                slot.transform.name = "itemTMP shield";
            }
            if (parchemin1 != null)
            {
                slot = Instantiate(prefabs, transform.position, transform.rotation);
                slot.transform.SetParent(holderSlot.transform);
                slot.GetComponent<TextMeshProUGUI>().text = parchemin1.nom + " - Parchemin 1";
                slot.GetComponent<UseItem>().item = parchemin1;
                slot.transform.name = "itemTMP parchemin1";
            }
            if (parchemin2 != null)
            {
                slot = Instantiate(prefabs, transform.position, transform.rotation);
                slot.transform.SetParent(holderSlot.transform);
                slot.GetComponent<TextMeshProUGUI>().text = parchemin2.nom + " - Parchemin 2";
                slot.GetComponent<UseItem>().item = parchemin2;
                slot.transform.name = "itemTMP parchemin2";
            }
            if (parchemin3 != null)
            {
                slot = Instantiate(prefabs, transform.position, transform.rotation);
                slot.transform.SetParent(holderSlot.transform);
                slot.GetComponent<TextMeshProUGUI>().text = parchemin3.nom + " - Parchemin 3";
                slot.GetComponent<UseItem>().item = parchemin3;
                slot.transform.name = "itemTMP parchemin3";
            }
            if (relique != null)
            {
                slot = Instantiate(prefabs, transform.position, transform.rotation);
                slot.transform.SetParent(holderSlot.transform);
                slot.GetComponent<TextMeshProUGUI>().text = relique.nom + " - Relique active";
                slot.GetComponent<UseItem>().item = relique;
                slot.transform.name = "itemTMP Relique";
            }
            for (int i = 0; i < inventory.Count; i++)
            {
                slot = Instantiate(prefabs, transform.position, transform.rotation);
                slot.transform.SetParent(holderSlot.transform);
                slot.transform.name = "itemTMP" + i;

                if (inventory[i] != null )
                {
                    slot.GetComponent<TextMeshProUGUI>().text = inventory[i].nom;
                    slot.GetComponent<UseItem>().item = inventory[i];
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.V) && inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(false);
        }
    }

    public void Fire()
    {
        inventoryPanel.SetActive(true);
        if (holderSlot.transform.childCount > 0)
        {
            foreach (Transform item in holderSlot.transform)
            {
                Destroy(item.gameObject);
            }
        }

        if (sword != null)
        {
            slot = Instantiate(prefabs, transform.position, transform.rotation);
            slot.transform.SetParent(holderSlot.transform);
            slot.GetComponent<TextMeshProUGUI>().text = sword.nom + " - Épée active";
            slot.GetComponent<UseItem>().item = sword;
            slot.transform.name = "itemTMP sword";
        }
        if (shield != null)
        {
            slot = Instantiate(prefabs, transform.position, transform.rotation);
            slot.transform.SetParent(holderSlot.transform);
            slot.GetComponent<TextMeshProUGUI>().text = sword.nom + " - Bouclier actif";
            slot.GetComponent<UseItem>().item = shield;
            slot.transform.name = "itemTMP shield";
        }
        if (parchemin1 != null)
        {
            slot = Instantiate(prefabs, transform.position, transform.rotation);
            slot.transform.SetParent(holderSlot.transform);
            slot.GetComponent<TextMeshProUGUI>().text = parchemin1.nom + " - Parchemin 1";
            slot.GetComponent<UseItem>().item = parchemin1;
            slot.transform.name = "itemTMP parchemin1";
        }
        if (parchemin2 != null)
        {
            slot = Instantiate(prefabs, transform.position, transform.rotation);
            slot.transform.SetParent(holderSlot.transform);
            slot.GetComponent<TextMeshProUGUI>().text = parchemin2.nom + " - Parchemin 2";
            slot.GetComponent<UseItem>().item = parchemin2;
            slot.transform.name = "itemTMP parchemin2";
        }
        if (parchemin3 != null)
        {
            slot = Instantiate(prefabs, transform.position, transform.rotation);
            slot.transform.SetParent(holderSlot.transform);
            slot.GetComponent<TextMeshProUGUI>().text = parchemin3.nom + " - Parchemin 3";
            slot.GetComponent<UseItem>().item = parchemin3;
            slot.transform.name = "itemTMP parchemin3";
        }
        if (relique != null)
        {
            slot = Instantiate(prefabs, transform.position, transform.rotation);
            slot.transform.SetParent(holderSlot.transform);
            slot.GetComponent<TextMeshProUGUI>().text = relique.nom + " - Relique active";
            slot.GetComponent<UseItem>().item = relique;
            slot.transform.name = "itemTMP Relique";
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            slot = Instantiate(prefabs, transform.position, transform.rotation);
            slot.transform.SetParent(holderSlot.transform);
            slot.transform.name = "itemTMP" + i;

            if (inventory[i] != null)
            {
                slot.GetComponent<TextMeshProUGUI>().text = inventory[i].nom;
                slot.GetComponent<UseItem>().item = inventory[i];
            }
        }
    }
}
