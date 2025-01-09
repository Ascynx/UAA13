using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{
    public Transform Player;

    void Update()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        if (Vector3.Distance(Player.position, transform.position) < 1)
        {
            if (Player.position.z == -1)
            {
                Player.position = new Vector3(Player.position.x, Player.position.y, 9);
                Player.gameObject.layer = 7;
            } else
            {
                Player.position = new Vector3(Player.position.x, Player.position.y, -1);
                Player.gameObject.layer = 6;
            }
        }
    }
}
