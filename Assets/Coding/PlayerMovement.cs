using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float vitesse;
    public float baseVitesse;
    public Rigidbody2D ceci;
    Vector3 deplacement;
    bool inventaireIsOuvert;
    public GameObject inventaire;
    public GameObject canvas, templates;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(templates);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventaire.activeInHierarchy)
        {
            vitesse = baseVitesse;
            if (Input.GetKey(KeyCode.Keypad0))
            {
                vitesse += baseVitesse;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                deplacement = new Vector3(0, vitesse, 0);
                ceci.velocity = deplacement;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                deplacement = new Vector3(0, -vitesse, 0);
                ceci.velocity = deplacement;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                deplacement = new Vector3(vitesse, 0, 0);
                ceci.velocity = deplacement;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                deplacement = new Vector3(-vitesse, 0, 0);
                ceci.velocity = deplacement;
            }
            else ceci.velocity = new Vector3(0,0,0);
            transform.eulerAngles = new Vector3(
                0,
                0,
                0
            );
        }
    }
}
