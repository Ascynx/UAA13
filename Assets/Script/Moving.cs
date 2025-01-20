using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public MappingButton mapping;
    public float baseVitesse;
    float vitesse;
    public Rigidbody2D ceci;

    public Sprite Up;
    public Sprite Right;
    public Sprite Down;
    public Sprite Left;

    public SpriteRenderer ActSprite;
    // Start is called before the first frame update
    void Awake()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, 7, true);
    }


    // Update is called once per frame
    void Update()
    {
        vitesse = baseVitesse;
        if (Input.GetKey(mapping.Run))
        {
            vitesse += baseVitesse;
        }
        if (Input.GetKey(mapping.Up))
        {
            ceci.velocity = new Vector3(0, vitesse, 0);
            ActSprite.sprite = Up;
        }
        else if (Input.GetKey(mapping.Down))
        {
            ceci.velocity = new Vector3(0, -vitesse, 0);
            ActSprite.sprite = Down;
        }
        else if (Input.GetKey(mapping.Right))
        {
            ceci.velocity = new Vector3(vitesse, 0, 0);
            ActSprite.sprite = Right;
        }
        else if (Input.GetKey(mapping.Left))
        {
            ceci.velocity = new Vector3(-vitesse, 0, 0);
            ActSprite.sprite = Left;
        }
        else ceci.velocity = new Vector3(0, 0, 0);

        transform.rotation = new Quaternion(0,0,0,0);

    }
}
