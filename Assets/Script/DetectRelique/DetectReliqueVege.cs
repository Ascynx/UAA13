using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DetectReliqueVege : MonoBehaviour
{
    public Inventory Inventaire;
    void Update()
    {
        if (Inventaire.equippedRelique != null)
        {
            if (Inventaire.equippedRelique.id == 0)
            {
                transform.GetComponent<TilemapCollider2D>().enabled = false;
            }
            else
            {
                transform.GetComponent<TilemapCollider2D>().enabled = true;
            }
        }
    }
}
