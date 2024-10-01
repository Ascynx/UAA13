using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public ItemData item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
            Debug.Log("UwU");
        if (collision.gameObject.name == "Player")
        {
            Inventory.instance.inventory.Add(item);
            Destroy(gameObject);
        }
    }
}
