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


    private Transform player;
    private GameObject fightBG;
    private float distanceToPlayer;
    private void Update()
    {
        player = GameObject.Find("Player").transform;
        fightBG = GameObject.Find("Canvas").transform.Find("FightBackground").gameObject;
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            if (!fightBG.activeInHierarchy)
            {
                GetComponent<Rigidbody2D>().velocity = (player.position - transform.position).normalized;
                if (distanceToPlayer <= 1)
                {
                fightBG.GetComponent<Combat>().Fight(me, transform);
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

