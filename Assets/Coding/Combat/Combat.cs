using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    void initTurn(mob ennemi, mob player, Attaque attackPlayer)
    {
        Attaque attackEnnemi = ennemi.attack[Random.Range(0, 2)];
        turn(ennemi, player, attackEnnemi, attackPlayer)
    }
    void turn(mob ennemi, mob player, Attaque attackEnnemi, Attaque attackPlayer)
    {
        attaque(attackPlayer, player, ref ennemi);
        if (ennemi.pvactuel > 0)
        {
            apliqueEffect(attackPlayer.effet, ref ennemi);
            attaque(attackEnnemi, ennemi, ref player);
            if (player.pvactuel > 0)
            {
                apliqueEffect(attackEnnemi.effet, ref player);
            }
        }
    }

    void apliqueEffect(Attaque.effect effet, ref mob victime)
    {
        bool isnotboss = (victime.Type1 != mob.type.Boss || victime.Type2 != mob.type.Boss);
        bool isnotsemboss = (victime.Type1 != mob.type.Boss || victime.Type2 != mob.type.Boss) && (victime.Type1 != mob.type.SemiBoss || victime.Type2 != mob.type.SemiBoss);
        if (effet == Attaque.effect.None || victime.effet != mob.effect.None)
        {

        }
        if (effet == Attaque.effect.Paralize)
        {
            if (Random.Range(0, 10) == 0 && (victime.Type1 != mob.type.Elec || victime.Type2 != mob.type.Elec) && isnotboss)
            {
                victime.effet = mob.effect.Paralize;
            }
        } 
        else if (effet == Attaque.effect.Burn)
        {
            if (Random.Range(0, 10) == 0 && (victime.Type1 != mob.type.Fire || victime.Type2 != mob.type.Fire) && isnotboss)
            {
                victime.effet = mob.effect.Burn;
            }
        }
        else if(effet == Attaque.effect.Toxic && isnotboss)
        {
            if (Random.Range(0, 10) == 0)
            {
                victime.effet = mob.effect.Toxic;
            }
        }
        else if (effet == Attaque.effect.PowerWind)
        {
            if ((victime.Type1 == mob.type.Aerial || victime.Type2 == mob.type.Aerial) && isnotsemboss)
            {
                victime.pvactuel = 0;
            }
        }
        else if (effet == Attaque.effect.LowerPrecision)
        {
            if (Random.Range(0, 10) == 0 && isnotsemboss)
            {
                victime.effet = mob.effect.LowerPrecision;
            }
        }
        else if (effet == Attaque.effect.LowerPower)
        {
            if (Random.Range(0, 10) == 0 && isnotsemboss)
            {
                victime.effet = mob.effect.LowerPower;
            }
        }
        else if (effet == Attaque.effect.LowerDef)
        {
            if (Random.Range(0, 10) == 0 && isnotsemboss)
            {
                victime.effet = mob.effect.LowerDef;
            }
        }
    }
    void attaque(Attaque att, mob attaquant, ref mob victime)
    {
        int precision = att.precision;
        int power = att.power;
        if (attaquant.effet == mob.effect.LowerPrecision)
        {
            precision = precision / 2;
        } else if (attaquant.effet == mob.effect.LowerPower)
        {
            power = power / 2;
        }
        else if (victime.effet == mob.effect.LowerDef)
        {
            power = power * 2;
        }
        if (Random.Range(0, 100) <= precision)
        {
            if (attaquant.effet != mob.effect.Paralize || Random.Range(0, 1) == 0)
            {
                victime.pvactuel -= power;
            }
        }
    }
}
