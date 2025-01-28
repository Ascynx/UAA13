using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{
    void Update()
    {
        Transform player = Jeu.Instance.player.transform;

        Physics2D.IgnoreLayerCollision(6, 7, true);
        if (Vector3.Distance(player.position, transform.position) < 1)
        {
            if (player.position.z == -1)
            {
                player.position = new Vector3(player.position.x, player.position.y, 9);
                player.gameObject.layer = 7;
            } else
            {
                player.position = new Vector3(player.position.x, player.position.y, -1);
                player.gameObject.layer = 6;
            }
        }
    }
}
