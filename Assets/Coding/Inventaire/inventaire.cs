using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Inventaire : MonoBehaviour
{
    [SerializeReference] private GameObject inventaire;
    // Start is called before the first frame update
    void Start()
    {
        inventaire.SetActive(false);
        inventaire = GameObject.Find("Inventaire");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!inventaire.activeSelf)
            {
                inventaire.SetActive(true);
                Thread.Sleep(100);
            } 
            else
            {
                inventaire.SetActive(false);
                Thread.Sleep(100);
            }
        }
    }
}
