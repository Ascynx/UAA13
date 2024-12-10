using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ToOrFromDonjon : MonoBehaviour
{
    public bool to;
    public GameObject Player;
    public Rigidbody2D player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (to)
        {
            Player.transform.position += new Vector3(0, 0, 10);
            Player.layer = 8;
            player.excludeLayers += 7;
            player.excludeLayers -= 8;
        }
        else
        {
            Player.transform.position += new Vector3(0, 0, -10);
            Player.layer = 7;
            player.excludeLayers += 8;
            player.excludeLayers -= 7;
        }
    }
}
