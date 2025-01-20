using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{
    public Transform Player;

    void Update()
    {
        if (Vector3.Distance(Player.position, transform.position) < 0.5)
        {
            if (Player.position.z == -1)
            {
                Player.position = new Vector3(Player.position.x, Player.position.y, 9);
                Physics2D.IgnoreLayerCollision(gameObject.layer, 7, false);
                Physics2D.IgnoreLayerCollision(gameObject.layer, 6, true);
            } else
            {
                Player.position = new Vector3(Player.position.x, Player.position.y, -1);
                Physics2D.IgnoreLayerCollision(gameObject.layer, 6, false);
                Physics2D.IgnoreLayerCollision(gameObject.layer, 7, true);
            }
        }
    }
}
