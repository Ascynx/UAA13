using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{
    void Update()
    {
        Transform player = Jeu.Instance.player.transform;
        if (Vector3.Distance(player.position, transform.position) < 1)
        {
            ChangeZonePlayer(
                player,
                transform.position,
                visualLayer: player.position.z == -1 ? 7 : 6,
                physicalLayer: (int) player.position.z == -1 ? 9 : -1
                );
        }
    }

    public static void ChangeZonePlayer(Transform player, Vector3 newPos, int visualLayer, int physicalLayer)
    {
        Physics2D.IgnoreLayerCollision(player.gameObject.layer, visualLayer, true);

        player.position = new Vector3(newPos.x, newPos.y, physicalLayer);
        player.gameObject.layer = visualLayer;

        //La minimap n'est config que pour l'overworld, cache là si on est pas dans l'overworld.
        if (visualLayer != 6)
        {
            Jeu.Instance.miniMapController.OnLeaveOverworld();
        } else
        {
            Jeu.Instance.miniMapController.OnReEnterOverworld();
        }
    }
}
