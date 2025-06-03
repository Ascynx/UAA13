using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item item;

    private void Awake()
    {
        #if !UNITY_EDITOR
        //évite d'avoir des items de test dans le jeu final, sauf en dev.
        if (item.nom.StartsWith("Dev") && Jeu.Instance.preferenceIntegration.GetBoolPreference("inDev", false))
        {
            Destroy(gameObject);
        }
        #endif
    }

    private void Update()
    {
        GameObject playerGameObject = GameObject.Find("Player");
        if (!Jeu.Instance.playerProperties.Alive)
        {
            //le joueur est inactif.
            return;
        }
        if (Vector3.Distance(playerGameObject.transform.position, transform.position) < 1)
        {
            if (playerGameObject.GetComponent<Inventory>().AddItem(item))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
