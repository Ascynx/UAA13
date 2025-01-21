using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MobAI : MonoBehaviour
{
    public mob me;
    public float moveSpeed;
    public float detectionRange;

    public Combat combat;
    private Transform player;
    public GameObject fightBG;
    private float distanceToPlayer;
    private void Update()
    {
        player = GameObject.Find("Player").transform;
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            if (!fightBG.activeInHierarchy)
            {
                GetComponent<Rigidbody2D>().velocity = (player.position - transform.position).normalized;
                if (distanceToPlayer <= transform.localScale.x && distanceToPlayer <= transform.localScale.y)
                {
                    combat.Fight(me, transform);
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

