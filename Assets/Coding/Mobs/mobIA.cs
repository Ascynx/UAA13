using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MobAI : MonoBehaviour
{
    public float moveSpeed;
    public float detectionRange;
    private Transform player;
    public GameObject fightBG;
    public Combat fight;
    public mob me;
    public Transform ceci;

    public float distanceToPlayer;
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange && !fightBG.activeInHierarchy)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            if (distanceToPlayer <= 1)
            {
                fight.Fight(me, ref ceci);
            }
        }
        
    }
}

