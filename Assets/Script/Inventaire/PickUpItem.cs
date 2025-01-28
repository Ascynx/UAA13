using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item item;

    private void Update()
    {
        if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) < 1)
        {
            if (GameObject.Find("Player").GetComponent<Inventory>().AddItem(item))
            {
                Destroy(gameObject);
            }
        }
    }
}
