using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToOrFromDonjon : MonoBehaviour
{
    public string Destination;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TG");
        SceneManager.LoadScene(Destination); 
    }
}
