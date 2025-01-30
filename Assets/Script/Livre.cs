using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Livre : MonoBehaviour
{
    public MappingButton mapping;
    public Transform player;

    public string[] paragraphes;
    public TextMeshProUGUI textMeshPro;
    public GameObject textMerdePro;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(mapping.Interact))
        {
            if (Vector3.Distance(player.position, transform.position) < 2)
            {
                if (textMerdePro.gameObject.activeInHierarchy)
                {
                    textMerdePro.gameObject.SetActive(false);
                }
                else
                {
                    Lire();
                }
            }
        }
    }
    public void Lire()
    {
        textMerdePro.gameObject.SetActive(true);
        textMeshPro.text = "";
        foreach (var p in paragraphes)
        {
            textMeshPro.text += p + "\n\n     ";
        }

    }
}
