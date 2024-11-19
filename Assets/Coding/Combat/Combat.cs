using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Combat : MonoBehaviour
{
    private Transform Mob;
    private mob ennemi;
    public Transform FightBackground;
    public bool playerTurn;
    public int button;
    private mob attacking;
    public SpriteRenderer sprite;
    public Inventory inventaire;

    int playerPv;
    Attaque[] attack = new Attaque[4];
    mob.effect pEffect;

    public void Fight(mob attacking, ref Transform tattacking)
    {
        
        this.attacking = attacking;
        sprite.sprite = attacking.sprite;
        initAtt(inventaire.sword, out attack[0]);
        initAtt(inventaire.parchemin1, out attack[1]);
        initAtt(inventaire.parchemin2, out attack[2]);
        initAtt(inventaire.parchemin3, out attack[3]);
        FightBackground.gameObject.SetActive(true);


        for (int i = 0; i < 4; i++)
        {
            if (attack[i] is null)
            {
                attack[i] = new Attaque(Attaque.effect.None, 1, 99, 1, "Lutte");
            }
        }
        GameObject.Find("BST").GetComponent<TextMeshProUGUI>().text = attack[0].name;
        for (int i = 1; i < 4; i++)
        {
            GameObject.Find("B"+i+"T").GetComponent<TextMeshProUGUI>().text = attack[i].name;
        }
        playerPv = 100;
        attacking.pvactuel = attacking.pvmax;
        attacking.ResetPP();


        //do
        //{
            for (int i = 0; i < 4; i++)
            {
                if (attack[i].ppact == 0)
                {
                    attack[i] = new Attaque(Attaque.effect.None, 1, 99, 1, "Lutte");
                    if (i == 0) GameObject.Find("BST").GetComponent<TextMeshProUGUI>().text = attack[0].name;
                    else GameObject.Find("B" + i + "T").GetComponent<TextMeshProUGUI>().text = attack[i].name;
                }
            }
            for (int i = 0; i < attacking.attack.Length; i++)
            {
                if (attacking.attack[i].ppact == 0)
                {
                    attacking.attack[i] = new Attaque(Attaque.effect.None, 1, 99, 1, "Lutte");
                }
            }
            wait();
            //initTurn(attack[button]);

        //} while (playerPv > 0 && attacking.pvactuel > 0);
        if (playerPv <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
        }
        else
        {
            tattacking.gameObject.SetActive(false);
        }
        FightBackground.gameObject.SetActive(false);
    }

    public IEnumerator wait()
    {
        playerTurn = true;
        Debug.Log("A");
        yield return new WaitUntil(() => !playerTurn);
        Debug.Log("B");
    }

    public void initAtt(ItemData enter, out Attaque sortie)
    {

        if (enter != null)
        {
            sortie = enter.attaque;
        }
        else
        {

            sortie = new Attaque(Attaque.effect.None, 1, 99, 1, "Lutte");
        }
    }
    void initTurn(Attaque attackPlayer)
    {
        Attaque attackEnnemi;
        do
        {
            attackEnnemi = attacking.attack[UnityEngine.Random.Range(0, attacking.attack.Length)];
        } while (attackEnnemi.ppact > 0);
        turn(attackEnnemi, attackPlayer);
    }
    void turn(Attaque attackEnnemi, Attaque attackPlayer)
    {
        attaque(attackPlayer, pEffect, attacking.effet, ref attacking.pvactuel);
        if (attacking.pvactuel > 0)
        {
            apliqueEffect(attackPlayer.effet,  attacking.Type1,  attacking.Type2, ref attacking.effet, ref attacking.pvactuel);
            attaque(attackEnnemi, attacking.effet, pEffect, ref playerPv);
            if (playerPv > 0)
            {
                apliqueEffect(attackEnnemi.effet, mob.type.None, mob.type.None, ref pEffect, ref playerPv);
                if (attacking.pvactuel > 0)
                {
                    apliqueEtat(attacking.pvmax, ref attacking.effet,ref attacking.pvactuel); 
                    if (attacking                                                                           .pvactuel > 0)
                    {
                        apliqueEtat(100, ref pEffect, ref playerPv);
                    }
                }
            }
        }
    }
    void apliqueEffect(Attaque.effect effet, mob.type Type1, mob.type Type2, ref mob.effect effect, ref int pv )
    {
        bool isnotboss = (Type1 != mob.type.Boss || Type2 != mob.type.Boss);
        bool isnotsemboss = (Type1 != mob.type.Boss || Type2 != mob.type.Boss) && (Type1 != mob.type.SemiBoss || Type2 != mob.type.SemiBoss);
        if (effet == Attaque.effect.None || effect != mob.effect.None)
        {

        } else 
        if (effet == Attaque.effect.Paralize)
        {
            if (UnityEngine.Random.Range(0, 10) == 0 && (Type1 != mob.type.Elec || Type2 != mob.type.Elec) && isnotboss)
            {
                effect = mob.effect.Paralize;
            }
        } 
        else if (effet == Attaque.effect.Burn)
        {
            if (UnityEngine.Random.Range(0, 10) == 0 && (Type1 != mob.type.Fire || Type2 != mob.type.Fire) && isnotboss)
            {
                effect = mob.effect.Burn;
            }
        }
        else if(effet == Attaque.effect.Toxic && isnotboss)
        {
            if (    UnityEngine.Random.Range(0, 10) == 0)
            {
                effect = mob.effect.Toxic;
            }
        }
        else if (effet == Attaque.effect.PowerWind)
        {
            if ((Type1 == mob.type.Aerial || Type2 == mob.type.Aerial) && isnotsemboss)
            {
                pv = 0;
            }
        }
        else if (effet == Attaque.effect.LowerPrecision)
        {
            if (UnityEngine.Random.Range(0, 10) == 0 && isnotsemboss)
            {
                effect = mob.effect.LowerPrecision;
            }
        }
        else if (effet == Attaque.effect.LowerPower)
        {
            if (UnityEngine.Random.Range(0, 10) == 0 && isnotsemboss)
            {
                effect = mob.effect.LowerPower;
            }
        }
        else if (effet == Attaque.effect.LowerDef)
        {
            if (UnityEngine.Random.Range(0, 10) == 0 && isnotsemboss)
            {
                effect = mob.effect.LowerDef;
            }
        }
    }
    void attaque(Attaque att, mob.effect effetA, mob.effect effetE, ref int pv)     
    {
        int precision = att.precision;
        int power = att.power;
        if (effetA == mob.effect.LowerPrecision)
        {
            precision = precision / 2;
        } else if (effetA == mob.effect.LowerPower)
        {
            power = power / 2;
        }
        else if (effetE == mob.effect.LowerDef)
        {
            power = power * 2;
        }
        if (UnityEngine.Random.Range(0, 100) <= precision)
        {
            if (effetA != mob.effect.Paralize || UnityEngine.Random.Range(0, 1) == 0)
            {
                pv -= power;
            }
        }
    }
    void apliqueEtat(int pvMax, ref mob.effect effet, ref int pv)
    {
        if (effet == mob.effect.Burn)
        {
            pv -= (pvMax / 10);
        }
        else if (effet == mob.effect.Toxic)
        {
            pv -= (pvMax / 15);
        }
    }
}
