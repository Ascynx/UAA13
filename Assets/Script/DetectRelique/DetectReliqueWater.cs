using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DetectReliqueWater : MonoBehaviour
{
    public Inventory Inventaire;
    void Update()
    {
        if (Inventaire.equippedRelique != null)
        {
            if (Inventaire.equippedRelique.id == 2)
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
