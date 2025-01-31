using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Combat : MonoBehaviour
{
    public Transform FightBackground;
    mob attacking;
    Transform tattacking;
    public Inventory inventaire;
    public TextMeshProUGUI textPlayerPV, textMobPV;

    int playerPv;
    public Attaque[] attack = new Attaque[4];
    mob.effect pEffect;


    public void Fight(mob attacking, Transform tattacking)
    {
        var t = inventaire.equippedSword;
        textPlayerPV.color = Color.white;
        pEffect = mob.effect.None;
        attacking.effet = mob.effect.None;
        textMobPV.color = Color.white;
        this.attacking = attacking;
        this.tattacking = tattacking;
        if (inventaire.equippedSword != null)
        {
            initAtt(inventaire.equippedSword, out attack[0]);
        }
        else
        {
            attack[0] = new Attaque(Attaque.effect.None, 10, 99, 1, "Lutte");
        }
        if (inventaire.equippedParchemins.Count >=1)
        {
            if (inventaire.equippedParchemins[0] != null)
            {
                initAtt(inventaire.equippedParchemins[0], out attack[1]);
            }
            else
            {
                attack[1] = new Attaque(Attaque.effect.None, 10, 99, 1, "Lutte");
            }
        }
        else
        {
            attack[1] = new Attaque(Attaque.effect.None, 10, 99, 1, "Lutte");
        }
        if (inventaire.equippedParchemins.Count >= 2)
        {
            if (inventaire.equippedParchemins[1] != null)
            {
                initAtt(inventaire.equippedParchemins[1], out attack[2]);
            }
            else
            {
                attack[2] = new Attaque(Attaque.effect.None, 10, 99, 1, "Lutte");
            }
        }
        else
        {
            attack[2] = new Attaque(Attaque.effect.None, 10, 99, 1, "Lutte");
        }
        if (inventaire.equippedParchemins.Count >= 3)
        {
            if (inventaire.equippedParchemins[2] != null)
            {
                initAtt(inventaire.equippedParchemins[2], out attack[3]);
            }
            else
            {
                attack[3] = new Attaque(Attaque.effect.None, 10, 99, 1, "Lutte");
            }
        }
        else
        {
            attack[3] = new Attaque(Attaque.effect.None, 10, 99, 1, "Lutte");
        }
        FightBackground.gameObject.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            if (i == 0) GameObject.Find("BST").GetComponentInChildren<TextMeshProUGUI>().text = attack[0].nom;
            else GameObject.Find("B" + i + "T").GetComponentInChildren<TextMeshProUGUI>().text = attack[i].nom;
        }
        playerPv = 100;
        attacking.pvactuel = attacking.pvmax;
        textMobPV.text = attacking.pvactuel.ToString() + "/" + attacking.pvmax.ToString() + " PV";
        textPlayerPV.text = "100/100 PV";
        attacking.pvactuel = attacking.pvmax;
        attacking.ResetPP();
        Debug.Log("att.ToString()");
    }
    public void FightAdvence(int att)
    {
        for (int i = 0; i < 4; i++)
        {
            if (attack[i].ppact == 0)
            {
                attack[i] = new Attaque(Attaque.effect.None, 10, 99, 1, "Lutte");
            }
            if (i == 0) GameObject.Find("BST").GetComponentInChildren<TextMeshProUGUI>().text = attack[0].nom;
            else GameObject.Find("B" + i + "T").GetComponentInChildren<TextMeshProUGUI>().text = attack[i].nom;
        }
        for (int i = 0; i < attacking.attack.Length; i++)
        {
            if (attacking.attack[i].ppact == 0)
            {
                attacking.attack[i] = new Attaque(Attaque.effect.None, 10, 99, 1, "Lutte");
            }
        }
        initTurn(attack[att]);
        if (playerPv <= 0 || attacking.pvactuel <= 0)
        {
            if (playerPv <= 0)
            {

            }
            else
            {
                tattacking.gameObject.SetActive(false);
                FightBackground.gameObject.SetActive(false);
            }
        }
        else
        {
            textPlayerPV.text = playerPv.ToString() + "/100 PV";
            textMobPV.text = attacking.pvactuel.ToString() + "/" + attacking.pvmax.ToString() + " PV";

            switch (attacking.effet)
            {
                case mob.effect.Paralize:
                    textMobPV.color = Color.yellow;
                    break;
                case mob.effect.Burn:
                    textMobPV.color = Color.red;
                    break;
                case mob.effect.Toxic:
                    textMobPV.color = Color.magenta;
                    break;
                case mob.effect.LowerPrecision:
                    textMobPV.color = Color.gray;
                    break;
                case mob.effect.LowerPower:
                    textMobPV.color = Color.green;
                    break;
                case mob.effect.LowerDef:
                    textMobPV.color = Color.blue;
                    break;
                case mob.effect.None:
                    textMobPV.color = Color.white;
                    break;
                default:
                    break;
            }
            switch (pEffect)
            {
                case mob.effect.Paralize:
                    textPlayerPV.color = Color.yellow;
                    break;
                case mob.effect.Burn:
                    textPlayerPV.color = Color.red;
                    break;
                case mob.effect.Toxic:
                    textPlayerPV.color = Color.magenta;
                    break;
                case mob.effect.LowerPrecision:
                    textPlayerPV.color = Color.gray;
                    break;
                case mob.effect.LowerPower:
                    textPlayerPV.color = Color.green;
                    break;
                case mob.effect.LowerDef:
                    textPlayerPV.color = Color.blue;
                    break;
                case mob.effect.None:
                    textPlayerPV.color = Color.white;
                    break;
                default:
                    break;
            }
        }
    }
    void initAtt(Item enter, out Attaque sortie)
    {
            if (enter.GetType().ToString()=="Sword")
            {
                Sword sword = (Sword)enter;
                sortie = sword.attaque;
            }
            else
            {
                Parchemin parchemin = (Parchemin)enter;
                sortie = parchemin.attaque;
        }
            sortie.ppact = sortie.ppmax;
    }
    void initTurn(Attaque attackPlayer)
    {
        Attaque attackEnnemi;
        attackEnnemi = attacking.attack[(int)UnityEngine.Random.Range(0, attacking.attack.Length-1)];
        turn(attackEnnemi, attackPlayer);
    }
    void turn(Attaque attackEnnemi, Attaque attackPlayer)
    {
        attaque(attackPlayer, pEffect, attacking.effet, attacking.def, ref attacking.pvactuel);
        if (attacking.pvactuel > 0)
        {
            apliqueEffect(attackPlayer.effet, attacking.Type1, attacking.Type2, ref attacking.effet, ref attacking.pvactuel);
            if (inventaire.equippedShield != null)
            {
                attaque(attackEnnemi, attacking.effet, pEffect, inventaire.equippedShield.def, ref playerPv);
            }
            else
            {
                attaque(attackEnnemi, attacking.effet, pEffect, 0, ref playerPv);
            }
            if (playerPv > 0)
            {
                apliqueEffect(attackEnnemi.effet, mob.type.None, mob.type.None, ref pEffect, ref playerPv);
                if (attacking.pvactuel > 0)
                {
                    apliqueEtat(attacking.pvmax, ref attacking.effet, ref attacking.pvactuel);
                    if (attacking.pvactuel > 0)
                    {
                        apliqueEtat(100, ref pEffect, ref playerPv);
                    }
                }
            }
        }
    }
    void apliqueEffect(Attaque.effect effet, mob.type Type1, mob.type Type2, ref mob.effect effect, ref int pv)
    {
        bool isnotboss = (Type1 != mob.type.Boss || Type2 != mob.type.Boss);
        bool isnotsemboss = (Type1 != mob.type.Boss || Type2 != mob.type.Boss) && (Type1 != mob.type.SemiBoss || Type2 != mob.type.SemiBoss);
        if (effet == Attaque.effect.None || effect != mob.effect.None)
        {

        }
        else if (effet == Attaque.effect.Burn)
        {
            if (UnityEngine.Random.Range(0, 10) == 0 && (Type1 != mob.type.Fire || Type2 != mob.type.Fire) && isnotboss)
            {
                effect = mob.effect.Burn;
            }
        }
        else
        if (effet == Attaque.effect.Paralize)
        {
            if (UnityEngine.Random.Range(0, 10) == 0 && (Type1 != mob.type.Elec || Type2 != mob.type.Elec) && isnotboss)
            {
                effect = mob.effect.Paralize;
            }
        }
        else if (effet == Attaque.effect.Toxic && isnotboss)
        {
            effect = mob.effect.Toxic;
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
    void attaque(Attaque att, mob.effect effetA, mob.effect effetE, int def, ref int pv)
    {
        int precision = att.precision;
        int power = att.power;
        if (effetA == mob.effect.LowerPrecision)
        {
            precision = precision / 2;
        }
        else if (effetA == mob.effect.LowerPower)
        {
            power = power / 2;
        }
        else if (effetE == mob.effect.LowerDef)
        {
            def = def - 1;
        }
        if (UnityEngine.Random.Range(0, 100) <= precision)
        {
            if (effetA != mob.effect.Paralize || UnityEngine.Random.Range(0, 1) == 0)
            {
                if (power - def >= 0)
                {
                    pv = pv - (power - def);
                }
            }
        }
    }
    void apliqueEtat(int pvMax, ref mob.effect effet, ref int pv)
    {
        if (effet == mob.effect.Burn)
        {
            pv -=  10;
        }
        else if (effet == mob.effect.Toxic)
        {
            pv -= (pvMax / 10);
        }
    }
}

