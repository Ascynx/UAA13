using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MobAI : MonoBehaviour
{
    public Mob me;
    public float moveSpeed;
    public float detectionRange;

    public Combat combat;
    private Transform player;
    public GameObject fightBG;
    private float distanceToPlayer;
    private void Update()
    {
        GameObject playerGameObject = GameObject.Find("Player");
        if (!Jeu.Instance.playerProperties.Alive)
        {
            //le joueur est inactif.
            return;
        }
        player = playerGameObject.transform;
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            if (!fightBG.activeInHierarchy)
            {
                if (moveSpeed != -1)
                {
                    GetComponent<Rigidbody2D>().velocity = (player.position - transform.position).normalized;
                }
                if (distanceToPlayer <= gameObject.GetComponent<SpriteRenderer>().bounds.size.x*2)
                {
                    combat.Fight(me, transform);
                    Jeu.Instance.OpenGUI(combat);
                }
            }
        else GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        } else GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        transform.eulerAngles = new Vector3(
                0,
                0,
                0
            );
    }
}

