using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Combat : MonoBehaviour
{
    public mob player;
    private Transform Mob;
    private mob ennemi;
    public Transform FightBackground;
    public bool playerTurn;
    public int button;

    void Fight(mob attacking, ref Transform tattacking)
    {
        FightBackground.gameObject.SetActive(true);
        Mob = tattacking;
        ennemi = attacking;
        player.pvactuel = player.pvmax;
        player.manaactuel = player.manamax;
        ennemi.pvactuel = ennemi.pvmax;
        ennemi.manaactuel = ennemi.manamax;
        do
        {
            playerTurn = true;
            do
            {

            } while (playerTurn);
            switch (button)
            {
                case 0:
                    initTurn(player.attack[0]);
                    break;
                case 1:
                    initTurn(player.attack[1]);
                    break;
                case 2:
                    initTurn(player.attack[2]);
                    break;
                case 3:
                    initTurn(player.attack[3]);
                    break;
            }

        } while (player.pvactuel > 0 && ennemi.pvactuel > 0);
        if (player.pvactuel <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
        }
        else
        {
            player.manaactuel = player.manamax;
            player.pvactuel = player.pvmax;
            tattacking.gameObject.SetActive(false);
        }
        FightBackground.gameObject.SetActive(false);
    }
    void initTurn(Attaque attackPlayer)
    {
        Attaque attackEnnemi;
        do
        {
            attackEnnemi = ennemi.attack[Random.Range(0, 2)];
        } while (attackEnnemi.costmana > ennemi.manaactuel);
        turn(attackEnnemi, attackPlayer);
    }
    void turn(Attaque attackEnnemi, Attaque attackPlayer)
    {
        attaque(attackPlayer, player, ref ennemi);
        if (ennemi.pvactuel > 0)
        {
            apliqueEffect(attackPlayer.effet, ref ennemi);
            attaque(attackEnnemi, ennemi, ref player);
            if (player.pvactuel > 0)
            {
                apliqueEffect(attackEnnemi.effet, ref player);
                if (ennemi.pvactuel > 0)
                {
                    apliqueEtat(ref ennemi); 
                    if (ennemi.pvactuel > 0)
                    {
                        apliqueEtat(ref player);
                    }
                }
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
    void apliqueEtat(ref mob victime)
    {
        if (victime.effet == mob.effect.Burn)
        {
            victime.pvmax -= (victime.pvmax/10);
        }
        else if (victime.effet == mob.effect.Toxic)
        {
            victime.pvmax -= (victime.pvmax / 15);
        }
    }
}
