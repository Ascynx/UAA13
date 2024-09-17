using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 oezhcv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            oezhcv = new Vector3(0, 0.025F, 0);
            transform.position = transform.position + oezhcv;
            Thread.Sleep(1);
        } 
        if (Input.GetKey(KeyCode.DownArrow))
        {
            oezhcv = new Vector3(0, -0.025F, 0);
            transform.position = transform.position + oezhcv;
            Thread.Sleep(1);
        } 
        if (Input.GetKey(KeyCode.RightArrow))
        {
            oezhcv = new Vector3(0.025F, 0, 0);
            transform.position = transform.position + oezhcv;
            Thread.Sleep(1);
        } 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            oezhcv = new Vector3(-0.025F, 0, 0);
            transform.position = transform.position + oezhcv;
            Thread.Sleep(1);
        }
        transform.eulerAngles = new Vector3(
            0,
            0,
            0
        );
    }

    void OnTriggerStay(Collider Tilemap)
    {
        
        transform.position = transform.position - oezhcv;
    }
}
