using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 deplacement;
    bool inventaireIsOuvert;
    public GameObject inventaire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventaire.activeInHierarchy)
        {
            Thread.Sleep(10);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                deplacement = new Vector3(0, 0.1F, 0);
                transform.position = transform.position + deplacement;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                deplacement = new Vector3(0, -0.1F, 0);
                transform.position = transform.position + deplacement;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                deplacement = new Vector3(0.1F, 0, 0);
                transform.position = transform.position + deplacement;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                deplacement = new Vector3(-0.1F, 0, 0);
                transform.position = transform.position + deplacement;
            }
            transform.eulerAngles = new Vector3(
                0,
                0,
                0
            );
        }
    }
}
