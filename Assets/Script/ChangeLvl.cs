using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{
    public Vector3 positionPostTeleport;

    void Update()
    {
        Transform player = Jeu.Instance.player.transform;

        Physics2D.IgnoreLayerCollision(6, 7, true);
        if (Vector3.Distance(player.position, transform.position) < 1)
        {
            if (player.position.z == -1)
            {
                player.position = positionPostTeleport;
                player.gameObject.layer = 7;
            } else
            {
                player.position = positionPostTeleport;
                player.gameObject.layer = 6;
            }
        }
    }
}
